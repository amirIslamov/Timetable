namespace Model.Validation.Abstractions;

public interface IValidator<in TEntity>
{
    Task<Result<IValidationResult>> ValidateAsync(TEntity entity);
}