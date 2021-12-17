namespace Model.Validation.Abstractions;

public interface IValidationError
{
    public string Code { get; }
    public string Description { get; }
}