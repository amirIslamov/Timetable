using Arch.EntityFrameworkCore.UnitOfWork;
using Model.Dal;
using Model.Entities;
using Model.Profile.Roles;
using Model.Validation.Abstractions;

namespace Model.Validation;

public class StudentValidator : IValidator<Student>
{
    private IUnitOfWork<TimetableDbContext> _unitOfWork;
    private TimetableErrorDescriber _describer;

    public StudentValidator(IUnitOfWork<TimetableDbContext> unitOfWork, TimetableErrorDescriber describer)
    {
        _unitOfWork = unitOfWork;
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
        var user = await _unitOfWork
            .GetRepository<TimetableUser>()
            .FindAsync(new {Id = student.Id});

        if (user == null)
        {
            errors.Add(_describer.InvalidUserId(student.Id));
            return;
        }
        
        if (user.RoleSet.ContainsRole(Role.Student))
            errors.Add(_describer.DuplicateStudent(student.Id));
    }
    
    public async Task ValidateGroup(Student student, IList<IValidationError> errors)
    {
        var group = await _unitOfWork
            .GetRepository<Group>()
            .FindAsync(new {Id = student.GroupId});
        
        if (group == null) 
            errors.Add(_describer.InvalidGroupId(student.GroupId));
    } 
}