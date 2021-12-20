using Arch.EntityFrameworkCore.UnitOfWork;
using Model.Dal;
using Model.Entities;
using Model.Profile.Roles;
using Model.Validation.Abstractions;

namespace Model.Validation;

public class StudentValidator : IValidator<Student>
{
    private IRepositoryFactory _repositoryFactory;
    private TimetableErrorDescriber _describer;

    public StudentValidator(IRepositoryFactory repositoryFactory, TimetableErrorDescriber describer)
    {
        _repositoryFactory = repositoryFactory;
        _describer = describer;
    }

    public async Task<Result<IValidationResult>> ValidateAsync(Student entity)
    {
        var errors = new List<IValidationError>();

        await ValidateGroup(entity, errors);
        await ValidateUser(entity, errors);

        if (errors.Count == 0) return Result<IValidationResult>.Create();

        return Result<IValidationResult>.Failed(new ValidationResult
        {
            Errors = errors
        });
    }

    public async Task ValidateUser(Student student, IList<IValidationError> errors)
    {
        var user = await _repositoryFactory
            .GetRepository<TimetableUser>()
            .FindAsync(student.UserId);

        if (user == null)
        {
            errors.Add(_describer.InvalidUserId(student.UserId));
            return;
        }
        
        var sameStudent = await _repositoryFactory
            .GetRepository<Teacher>()
            .GetFirstOrDefaultAsync(
                predicate: s => s.Id != student.Id && s.UserId == student.UserId);
        
        if (sameStudent != null)
            errors.Add(_describer.DuplicateStudent(student.UserId));
    }
    
    public async Task ValidateGroup(Student student, IList<IValidationError> errors)
    {
        var group = await _repositoryFactory
            .GetRepository<Group>()
            .FindAsync(student.GroupId);
        
        if (group == null) 
            errors.Add(_describer.InvalidGroupId(student.GroupId));
    } 
}