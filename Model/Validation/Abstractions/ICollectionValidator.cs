namespace Model.Validation.Abstractions;

public interface ICollectionValidator<in TEntity, TKey>
{
    Result<ICollectionValidationResult<TKey>> ValidateRangeAsync(IEnumerable<TEntity> entities);
}