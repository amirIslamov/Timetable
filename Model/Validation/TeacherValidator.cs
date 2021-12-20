using Arch.EntityFrameworkCore.UnitOfWork;
using Model.Dal;
using Model.Entities;
using Model.Profile.Roles;
using Model.Validation.Abstractions;

namespace Model.Validation;

public class TeacherValidator : IValidator<Teacher>
{
    private IRepositoryFactory _repositoryFactory;
    private TimetableErrorDescriber _describer;

    public TeacherValidator(IRepositoryFactory repositoryFactory, TimetableErrorDescriber describer)
    {
        _repositoryFactory = repositoryFactory;
        _describer = describer;
    }

    public async Task<Result<IValidationResult>> ValidateAsync(Teacher entity)
    {
        var errors = new List<IValidationError>();

        await ValidateUser(entity, errors);

        if (errors.Count == 0) return Result<IValidationResult>.Create();

        return Result<IValidationResult>.Failed(new ValidationResult
        {
            Errors = errors
        });
    }
    
    public async Task ValidateUser(Teacher teacher, IList<IValidationError> errors)
    {
        var user = await _repositoryFactory
            .GetRepository<TimetableUser>()
            .FindAsync(teacher.UserId);

        if (user == null)
        {
            errors.Add(_describer.InvalidUserId(teacher.UserId));
            return;
        }

        var sameTeacher = await _repositoryFactory
            .GetRepository<Teacher>()
            .GetFirstOrDefaultAsync(
                predicate: t => t.Id != teacher.Id && t.UserId == teacher.UserId);
        
        if (sameTeacher != null)
            errors.Add(_describer.DuplicateTeacher(teacher.UserId));
    }
}