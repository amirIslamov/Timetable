namespace Model.Validation.Abstractions;

public interface ICollectionValidationResult<TKey>
{
    IList<KeyedValidationBag> Errors { get; }
    IValidationResult AggregateErrors { get; }

    public class KeyedValidationBag
    {
        private TKey Key { get; set; }
        private IValidationResult ValidationResult { get; set; }
    }
}

internal class CollectionValidationResult<TKey> : ICollectionValidationResult<TKey>
{
    public CollectionValidationResult(IList<ICollectionValidationResult<TKey>.KeyedValidationBag> errors,
        IValidationResult aggregateErrors)
    {
        Errors = errors;
        AggregateErrors = aggregateErrors;
    }

    public IList<ICollectionValidationResult<TKey>.KeyedValidationBag> Errors { get; }
    public IValidationResult AggregateErrors { get; }
}