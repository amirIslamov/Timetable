using Model.Entities;
using Model.Validation.Abstractions;

namespace Model.Auth;

public class UserCollectionValidator : ICollectionValidator<TimetableUser, long>
{
    public Result<ICollectionValidationResult<long>> ValidateRangeAsync(IEnumerable<TimetableUser> entities)
    {
        throw new NotImplementedException();
    }
}