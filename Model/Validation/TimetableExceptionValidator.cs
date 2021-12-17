using Arch.EntityFrameworkCore.UnitOfWork;
using Model.Entities;
using Model.Validation.Abstractions;

namespace Model.Validation;

public class TimetableExceptionValidator : IValidator<TimetableException>
{
    private readonly TimetableErrorDescriber _describer;
    private readonly IRepositoryFactory _repositoryFactory;

    public TimetableExceptionValidator(IRepositoryFactory repositoryFactory, TimetableErrorDescriber describer)
    {
        _repositoryFactory = repositoryFactory;
        _describer = describer;
    }

    public async Task<Result<IValidationResult>> ValidateAsync(TimetableException entity)
    {
        throw new NotImplementedException();
    }
}