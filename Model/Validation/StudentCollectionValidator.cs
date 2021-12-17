using Model.Entities;
using Model.Validation.Abstractions;

namespace Model.Validation;

public class StudentCollectionValidator : ICollectionValidator<Student, long>
{
    public Result<ICollectionValidationResult<long>> ValidateRangeAsync(IEnumerable<Student> entities)
    {
        throw new NotImplementedException();
    }
}