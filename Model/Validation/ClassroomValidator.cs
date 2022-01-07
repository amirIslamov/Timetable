using Arch.EntityFrameworkCore.UnitOfWork;
using Model.Entities;
using Model.Validation.Abstractions;

namespace Model.Validation;

public class ClassroomValidator: IValidator<Classroom>
{
    private readonly TimetableErrorDescriber _describer;
    private readonly IRepositoryFactory _repositoryFactory;

    public ClassroomValidator(TimetableErrorDescriber describer, IRepositoryFactory repositoryFactory)
    {
        _describer = describer;
        _repositoryFactory = repositoryFactory;
    }

    public async Task<Result<IValidationResult>> ValidateAsync(Classroom entity)
    {
        var errors = new List<IValidationError>();

        var sameClassroom = await _repositoryFactory
            .GetRepository<Classroom>()
            .GetFirstOrDefaultAsync(predicate: c =>
                c.Pavilion == entity.Pavilion
                && c.ClassroomNumber == entity.ClassroomNumber);

        if (sameClassroom != null)
        {
            errors.Add(_describer.DuplicateClassroom(
                entity.Pavilion,
                entity.ClassroomNumber));
        }
        
        if (errors.Count == 0) return Result<IValidationResult>.Create();

        return Result<IValidationResult>.Failed(new ValidationResult
        {
            Errors = errors
        });
    }
}