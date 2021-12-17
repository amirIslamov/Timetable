using Arch.EntityFrameworkCore.UnitOfWork;
using Model.Entities;
using Model.Users;
using Model.Validation.Abstractions;

namespace Model.Auth;

public class PasswordManager
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IRepositoryFactory _repositoryFactory;
    private readonly PasswordValidator _validator;

    public PasswordManager(IPasswordHasher passwordHasher, IRepositoryFactory repositoryFactory,
        PasswordValidator validator)
    {
        _passwordHasher = passwordHasher;
        _repositoryFactory = repositoryFactory;
        _validator = validator;
    }

    public async Task<PasswordVerificationResult> CheckPasswordAsync(TimetableUser user, string providedPassword)
    {
        return _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, providedPassword);
    }

    public async Task<Result<IValidationResult>> SetPasswordAsync(TimetableUser user, string providedPasswod)
    {
        var validationResult = await _validator.ValidateAsync(providedPasswod);

        if (validationResult.Succeeded)
        {
            user.PasswordHash = _passwordHasher.HashPassword(user, providedPasswod);
            return Result<IValidationResult>.Create();
        }

        return validationResult;
    }

    public Task<bool> HasPassword(TimetableUser user)
    {
        return Task.FromResult(user.PasswordHash != null);
    }
}