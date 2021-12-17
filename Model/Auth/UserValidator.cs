using Arch.EntityFrameworkCore.UnitOfWork;
using Model.Entities;
using Model.Validation.Abstractions;

namespace Model.Auth;

public class UserValidator : IValidator<TimetableUser>
{
    private readonly AuthErrorDescriber _describer;
    private readonly IRepositoryFactory _repositoryFactory;

    public UserValidator(IRepositoryFactory repositoryFactory, AuthErrorDescriber describer)
    {
        _repositoryFactory = repositoryFactory;
        _describer = describer;
    }

    public async Task<Result<IValidationResult>> ValidateAsync(TimetableUser entity)
    {
        var errors = new List<IValidationError>();

        await ValidateEmail(entity, errors);

        if (errors.Count == 0) return Result<IValidationResult>.Create();

        return Result<IValidationResult>.Failed(new ValidationResult
        {
            Errors = errors
        });
    }

    public async Task ValidateEmail(TimetableUser user, IList<IValidationError> errors)
    {
        if (user.Email == null)
        {
            errors.Add(_describer.UserEmailNull());
            return;
        }

        var subjectWithSameCode = await _repositoryFactory
            .GetRepository<TimetableUser>()
            .GetFirstOrDefaultAsync(
                predicate: s => s.Id != user.Id && s.Email == user.Email);

        if (subjectWithSameCode != null) errors.Add(_describer.DuplicateEmail(user.Email));
    }
}