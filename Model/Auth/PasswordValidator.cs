using Microsoft.Extensions.Options;
using Model.Validation.Abstractions;

namespace Model.Auth;

public class PasswordValidator
{
    private readonly AuthErrorDescriber _describer;
    private readonly PasswordValidationOptions _options;

    public PasswordValidator(IOptions<PasswordValidationOptions> options, AuthErrorDescriber describer)
    {
        _describer = describer;
        _options = options.Value;
    }

    public async Task<Result<IValidationResult>> ValidateAsync(string password)
    {
        var errors = new List<IValidationError>();

        if (password.Length < _options.MinSize && password.Length > _options.MaxSize)
            errors.Add(_describer.IncorrectPasswordLength(_options.MinSize, _options.MaxSize));

        if (password.Any(c => !_options.AllowedCharacters.Contains(c)))
            errors.Add(_describer.PasswordContainsDisallowedCharacters());

        if (_options.RequireNonAlphanumeric && password.All(char.IsLetterOrDigit))
            errors.Add(_describer.PasswordDoesNotContainNonAlphanumericCharacters());

        if (_options.RequireDigit && !password.Any(char.IsDigit)) errors.Add(_describer.PasswordDoesNotContainDigits());

        if (errors.Count == 0) return Result<IValidationResult>.Create();

        return Result<IValidationResult>.Failed(new ValidationResult
        {
            Errors = errors
        });
    }
}