using Arch.EntityFrameworkCore.UnitOfWork;
using Model.Entities;
using Model.Validation.Abstractions;

namespace Model.Validation;

public class SubjectValidator : IValidator<Subject>
{
    private readonly TimetableErrorDescriber _describer;
    private readonly IRepositoryFactory _repositoryFactory;

    public SubjectValidator(IRepositoryFactory repositoryFactory, TimetableErrorDescriber describer)
    {
        _repositoryFactory = repositoryFactory;
        _describer = describer;
    }

    public async Task<Result<IValidationResult>> ValidateAsync(Subject entity)
    {
        var errors = new List<IValidationError>();

        await ValidateCode(entity, errors);
        await ValidateName(entity, errors);

        if (errors.Count == 0) return Result<IValidationResult>.Create();

        return Result<IValidationResult>.Failed(new ValidationResult
        {
            Errors = errors
        });
    }

    public async Task ValidateName(Subject subject, IList<IValidationError> errors)
    {
        if (subject.Name == null) errors.Add(_describer.SubjectNameNull());
    }

    public async Task ValidateCode(Subject subject, IList<IValidationError> errors)
    {
        if (subject.Code == null)
        {
            errors.Add(_describer.SubjectCodeNull());
            return;
        }

        var subjectWithSameCode = await _repositoryFactory
            .GetRepository<Subject>()
            .GetFirstOrDefaultAsync(
                predicate: s => s.Id != subject.Id && s.Code == subject.Code);

        if (subjectWithSameCode != null) errors.Add(_describer.DuplicateSubjectCode(subject.Code));
    }
}