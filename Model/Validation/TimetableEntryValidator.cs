using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Model.Validation.Abstractions;

namespace Model.Validation;

public class TimetableEntryValidator : IValidator<TimetableEntry>
{
    private readonly TimetableErrorDescriber _describer;
    private readonly IRepositoryFactory _repositoryFactory;

    public TimetableEntryValidator(IRepositoryFactory repositoryFactory, TimetableErrorDescriber describer)
    {
        _repositoryFactory = repositoryFactory;
        _describer = describer;
    }

    public async Task<Result<IValidationResult>> ValidateAsync(TimetableEntry entity)
    {
        var errors = new List<IValidationError>();

        await ValidateTime(entity, errors);
        await ValidateTeacherLoad(entity, errors);

        if (errors.Count == 0) return Result<IValidationResult>.Create();

        return Result<IValidationResult>.Failed(new ValidationResult
        {
            Errors = errors
        });
    }

    public async Task ValidateTime(TimetableEntry entry, IList<IValidationError> errors)
    {
        var teacherLoad = await _repositoryFactory
            .GetRepository<TeacherLoad>()
            .GetFirstOrDefaultAsync(
                predicate: l => l.Id == entry.TeacherLoadId,
                include: q => q.Include(l => l.Discipline));

        var entryAtTheSameTime = await _repositoryFactory
            .GetRepository<TimetableEntry>()
            .GetFirstOrDefaultAsync(
                predicate: e =>
                    e.TeacherLoad.Discipline.GroupId == teacherLoad.Discipline.GroupId
                    && e.WeekType == entry.WeekType
                    && e.DayOfWeek == entry.DayOfWeek
                    && e.ClassNum == entry.ClassNum);

        if (entryAtTheSameTime != null) errors.Add(_describer.EntryAtTheSameTimeExists(entry.TeacherLoadId));
    }

    public async Task ValidateTeacherLoad(TimetableEntry entry, IList<IValidationError> errors)
    {
        var teacherLoad = await _repositoryFactory
            .GetRepository<TeacherLoad>()
            .FindAsync(entry.TeacherLoadId);

        if (teacherLoad == null) errors.Add(_describer.InvalidTeacherLoadId(entry.TeacherLoadId));
    }
}