using Model.Entities;
using Model.Validation.Abstractions;

namespace Model.Validation;

public class TeacherValidator : IValidator<Teacher>
{
    public async Task<Result<IValidationResult>> ValidateAsync(Teacher entity)
    {
        throw new NotImplementedException();
    }
}