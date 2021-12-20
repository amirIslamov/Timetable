using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Model.Validation.Abstractions;

namespace Model.Validation;

public class LoadValidator : IValidator<TeacherLoad>
{
    private readonly TimetableErrorDescriber _describer;
    private readonly IRepositoryFactory _repositoryFactory;

    private Discipline _discipline;

    public LoadValidator(IRepositoryFactory repositoryFactory, TimetableErrorDescriber describer)
    {
        _repositoryFactory = repositoryFactory;
        _describer = describer;
    }

    public async Task<Result<IValidationResult>> ValidateAsync(TeacherLoad entity)
    {
        _discipline = await _repositoryFactory
            .GetRepository<Discipline>()
            .FindAsync(entity.DisciplineId);

        var errors = new List<IValidationError>();

        await ValidateDiscipline(entity, errors);
        await ValidateTeacher(entity, errors);
        await ValidateWorkHours(entity, errors);

        if (errors.Count == 0) return Result<IValidationResult>.Create();

        return Result<IValidationResult>.Failed(new ValidationResult
        {
            Errors = errors
        });
    }

    public async Task ValidateDiscipline(TeacherLoad teacherLoad, IList<IValidationError> errors)
    {
        if (_discipline == null) errors.Add(_describer.InvalidDisciplineId(teacherLoad.DisciplineId));
    }

    public async Task ValidateTeacher(TeacherLoad teacherLoad, IList<IValidationError> errors)
    {
        var groupCurator = await _repositoryFactory
            .GetRepository<Teacher>()
            .FindAsync(teacherLoad.DisciplineId);

        if (groupCurator == null) errors.Add(_describer.InvalidTeacherId(teacherLoad.DisciplineId));
    }

    public async Task ValidateWorkHours(TeacherLoad teacherLoad, IList<IValidationError> errors)
    {
        if (teacherLoad.TotalHours < 0)
        {
            errors.Add(_describer.InvalidLoadWorkHours(teacherLoad.TotalHours));
            return;
        }

        if (_discipline != null)
        {
            var totalLoadHours = await _repositoryFactory
                .GetRepository<TeacherLoad>()
                .GetAll()
                .Where(l => l.DisciplineId == _discipline.Id && l.Id != teacherLoad.Id)
                .SumAsync(l => l.TotalHours) + teacherLoad.TotalHours;

            if (totalLoadHours > _discipline.ClassroomWorkHours)
                errors.Add(_describer.NotEnoughWorkHours(teacherLoad.TotalHours));
        }
    }
}