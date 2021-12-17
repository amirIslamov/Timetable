using Model.Entities;
using Model.Validation.Abstractions;

namespace Model.Validation;

public class TeacherCollectionValidator : ICollectionValidator<Teacher, long>
{
    public Result<ICollectionValidationResult<long>> ValidateRangeAsync(IEnumerable<Teacher> entities)
    {
        throw new NotImplementedException();
    }
}