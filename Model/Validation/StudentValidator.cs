using Model.Entities;
using Model.Validation.Abstractions;

namespace Model.Validation;

public class StudentValidator : IValidator<Student>
{
    public async Task<Result<IValidationResult>> ValidateAsync(Student entity)
    {
        throw new NotImplementedException();
    }
}