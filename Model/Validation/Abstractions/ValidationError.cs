namespace Model.Validation.Abstractions;

public class ValidationError : IValidationError
{
    public ValidationError(string code, string description)
    {
        Code = code;
        Description = description;
    }

    public string Code { get; init; }
    public string Description { get; init; }
}