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

    private TeacherLoad _teacherLoad;

    public async Task<Result<IValidationResult>> ValidateAsync(TimetableEntry entity)
    {
        var errors = new List<IValidationError>();

        await Validate(entity, errors);
        
        if (errors.Count == 0) return Result<IValidationResult>.Create();

        return Result<IValidationResult>.Failed(new ValidationResult
        {
            Errors = errors
        });
    }

    public async Task Validate(TimetableEntry entry, IList<IValidationError> errors)
    {
        var teacherLoad = await _repositoryFactory
            .GetRepository<TeacherLoad>()
            .GetFirstOrDefaultAsync(
                predicate: l => l.Id == entry.TeacherLoadId,
                include: q => q.Include(l => l.Discipline));

        if (teacherLoad == null)
        {
            errors.Add(_describer.InvalidTeacherLoadId(entry.TeacherLoadId));
            return;
        }

        var entryAtTheSameTime = await _repositoryFactory
            .GetRepository<TimetableEntry>()
            .GetFirstOrDefaultAsync(
                predicate: e =>
                    e.WeekType == entry.WeekType
                    && e.DayOfWeek == entry.DayOfWeek
                    && e.ClassNum == entry.ClassNum
                    && e.GroupId == teacherLoad.Discipline.GroupId);

        if (entryAtTheSameTime != null) errors.Add(_describer.EntryAtTheSameTimeExists(entry.TeacherLoadId));
        entry.GroupId = _teacherLoad.Discipline.GroupId;
    }
}