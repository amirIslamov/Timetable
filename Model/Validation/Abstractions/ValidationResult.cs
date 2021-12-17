namespace Model.Validation.Abstractions;

public class ValidationResult : IValidationResult
{
    public IList<IValidationError> Errors { get; init; }
}