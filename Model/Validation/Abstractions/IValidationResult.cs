namespace Model.Validation.Abstractions;

public interface IValidationResult
{
    public IList<IValidationError> Errors { get; }
}