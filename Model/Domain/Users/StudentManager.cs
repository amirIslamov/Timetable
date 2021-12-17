using Arch.EntityFrameworkCore.UnitOfWork;
using Model.Entities;
using Model.Validation.Abstractions;

namespace Model.Domain.students;

public class StudentManager
{
    private readonly IRepositoryFactory _repositoryFactory;
    private readonly ICollectionValidator<Student, long> _studentCollectionValidator;
    private readonly IValidator<Student> _studentValidator;

    public StudentManager(IRepositoryFactory repositoryFactory, IValidator<Student> studentValidator,
        ICollectionValidator<Student, long> studentCollectionValidator)
    {
        _repositoryFactory = repositoryFactory;
        _studentValidator = studentValidator;
        _studentCollectionValidator = studentCollectionValidator;
    }

    private async Task<Result<IValidationResult>> CreateAsync(Student student)
    {
        var validationResult = await _studentValidator.ValidateAsync(student);

        if (validationResult.Succeeded)
        {
            await _repositoryFactory.GetRepository<Student>().InsertAsync(student);
            return Result<IValidationResult>.Create();
        }

        return validationResult;
    }

    private async Task<Result<ICollectionValidationResult<long>>> CreateRangeAsync(IEnumerable<Student> students)
    {
        var validationResult = _studentCollectionValidator.ValidateRangeAsync(students);

        if (validationResult.Succeeded)
        {
            await _repositoryFactory.GetRepository<Student>().InsertAsync(students);
            return Result<ICollectionValidationResult<long>>.Create();
        }

        return validationResult;
    }

    private async Task<Student> FindAsync(long id)
    {
        return await _repositoryFactory
            .GetRepository<Student>()
            .FindAsync(new {Id = id});
    }

    private async Task<Student> FindAsync(object id)
    {
        return await _repositoryFactory
            .GetRepository<Student>()
            .FindAsync(id);
    }

    private async Task UpdateAsync(Student student)
    {
        _repositoryFactory.GetRepository<Student>().Update(student);
    }

    private async Task UpdateRangeAsync(IEnumerable<Student> students)
    {
        _repositoryFactory.GetRepository<Student>().Update(students);
    }

    private async Task Remove(Student student)
    {
        _repositoryFactory.GetRepository<Student>().Delete(student);
    }

    private async Task RemoveById(object id)
    {
        _repositoryFactory.GetRepository<Student>().Delete(id);
    }

    private async Task RemoveRange(IEnumerable<Student> students)
    {
        _repositoryFactory.GetRepository<Student>().Delete(students);
    }

    private async Task RemoveRangeByIds(IEnumerable<object> ids)
    {
        _repositoryFactory.GetRepository<Student>().Delete(ids);
    }
}