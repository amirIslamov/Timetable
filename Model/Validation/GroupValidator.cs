using Arch.EntityFrameworkCore.UnitOfWork;
using Model.Entities;
using Model.Validation.Abstractions;

namespace Model.Validation;

public class GroupValidator : IValidator<Group>
{
    private readonly TimetableErrorDescriber _describer;
    private readonly IRepositoryFactory _repositoryFactory;

    public GroupValidator(IRepositoryFactory repositoryFactory, TimetableErrorDescriber describer)
    {
        _repositoryFactory = repositoryFactory;
        _describer = describer;
    }

    public async Task<Result<IValidationResult>> ValidateAsync(Group entity)
    {
        var errors = new List<IValidationError>();

        await ValidateName(entity, errors);
        await ValidateCurator(entity, errors);
        await ValidateAdmissionYear(entity, errors);
        await ValidateShortName(entity, errors);

        if (errors.Count == 0) return Result<IValidationResult>.Create();

        return Result<IValidationResult>.Failed(new ValidationResult
        {
            Errors = errors
        });
    }

    public async Task ValidateName(Group group, IList<IValidationError> errors)
    {
        if (group.Name == null) errors.Add(_describer.GroupNameNull());
    }

    public async Task ValidateShortName(Group group, IList<IValidationError> errors)
    {
        if (group.ShortName == null)
        {
            errors.Add(_describer.GroupShortNameNull());
            return;
        }

        var groupWithSameShortName = await _repositoryFactory
            .GetRepository<Group>()
            .GetFirstOrDefaultAsync(
                predicate: g => g.Id != group.Id && g.ShortName == group.ShortName);

        if (groupWithSameShortName != null) errors.Add(_describer.DuplicateGroupShortName(@group.ShortName));
    }

    public async Task ValidateAdmissionYear(Group group, IList<IValidationError> errors)
    {
        if (group.AdmissionYear <= 1980 || group.AdmissionYear > DateTime.Now.Year)
            errors.Add(_describer.InvalidAdmissionYear(@group.AdmissionYear));
    }

    public async Task ValidateCurator(Group group, IList<IValidationError> errors)
    {
        var groupCurator = await _repositoryFactory
            .GetRepository<Teacher>()
            .FindAsync(new {Id = group.CuratorId});

        if (groupCurator == null) errors.Add(_describer.InvalidCuratorId(@group.CuratorId));
    }
}