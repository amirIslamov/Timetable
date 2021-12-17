using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Model.Validation.Abstractions;

namespace Model.Validation;

public class DisciplineValidator : IValidator<Discipline>
{
    private readonly TimetableErrorDescriber _describer;
    private readonly IRepositoryFactory _repositoryFactory;

    public DisciplineValidator(IRepositoryFactory repositoryFactory, TimetableErrorDescriber describer)
    {
        _repositoryFactory = repositoryFactory;
        _describer = describer;
    }

    public async Task<Result<IValidationResult>> ValidateAsync(Discipline entity)
    {
        var errors = new List<IValidationError>();

        await ValidateGroup(entity, errors);
        await ValidateSubject(entity, errors);
        await ValidateSemesterNumber(entity, errors);
        await ValidateWorkHours(entity, errors);

        if (errors.Count == 0) return Result<IValidationResult>.Create();

        return Result<IValidationResult>.Failed(new ValidationResult
        {
            Errors = errors
        });
    }

    public async Task ValidateSubject(Discipline discipline, IList<IValidationError> errors)
    {
        var groupCurator = await _repositoryFactory
            .GetRepository<Subject>()
            .FindAsync(new {Id = discipline.SubjectId});

        if (groupCurator == null) errors.Add(_describer.InvalidSubjectId(discipline.SubjectId));
    }

    public async Task ValidateGroup(Discipline discipline, IList<IValidationError> errors)
    {
        var groupCurator = await _repositoryFactory
            .GetRepository<Group>()
            .FindAsync(new {Id = discipline.GroupId});

        if (groupCurator == null) errors.Add(_describer.InvalidGroupId(discipline.SubjectId));
    }

    public async Task ValidateSemesterNumber(Discipline discipline, IList<IValidationError> errors)
    {
        if (discipline.SemesterNumber > 8 || discipline.SemesterNumber < 0)
            errors.Add(_describer.InvalidSemesterNumber(discipline.SemesterNumber));
    }

    public async Task ValidateWorkHours(Discipline discipline, IList<IValidationError> errors)
    {
        if (discipline.ClassroomWorkHours < 0)
            errors.Add(_describer.InvalidClassroomWorkHours(discipline.ClassroomWorkHours));
        if (discipline.IndependentWorkHours < 0)
            errors.Add(_describer.InvalidIndependentWorkHours(discipline.IndependentWorkHours));

        var totalLoadHours =
            await _repositoryFactory
                .GetRepository<TeacherLoad>()
                .GetAll()
                .Where(l => l.DisciplineId == discipline.Id)
                .SumAsync(l => l.TotalHours);

        if (discipline.ClassroomWorkHours < totalLoadHours)
            errors.Add(_describer.InvalidClassroomWorkHoursTruncation(discipline.ClassroomWorkHours));
    }
}