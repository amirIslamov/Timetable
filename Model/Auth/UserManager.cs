using System.Security.Claims;
using Arch.EntityFrameworkCore.UnitOfWork;
using Model.Entities;
using Model.Validation.Abstractions;

namespace Model.Auth;

public class TimetableUserManager
{
    private readonly PasswordManager _passwordManager;
    private readonly IRepositoryFactory _repositoryFactory;
    private readonly ICollectionValidator<TimetableUser, long> _userCollectionValidator;
    private readonly IValidator<TimetableUser> _userValidator;

    public TimetableUserManager(IRepositoryFactory repositoryFactory, IValidator<TimetableUser> userValidator,
        ICollectionValidator<TimetableUser, long> userCollectionValidator, PasswordManager passwordManager)
    {
        _repositoryFactory = repositoryFactory;
        _userValidator = userValidator;
        _userCollectionValidator = userCollectionValidator;
        _passwordManager = passwordManager;
    }

    public async Task<Result<IValidationResult>> CreateAsync(TimetableUser user)
    {
        var validationResult = await _userValidator.ValidateAsync(user);

        if (validationResult.Succeeded)
        {
            await _repositoryFactory.GetRepository<TimetableUser>().InsertAsync(user);
            return Result<IValidationResult>.Create();
        }

        return validationResult;
    }

    public async Task<Result<IValidationResult>> CreateAsync(TimetableUser user, string password)
    {
        var result = await CreateAsync(user);

        if (result.Succeeded)
        {
            var passwordResult = await _passwordManager.SetPasswordAsync(user, password);

            return passwordResult;
        }

        return result;
    }

    public async Task<Result<ICollectionValidationResult<long>>> CreateRangeAsync(IEnumerable<TimetableUser> users)
    {
        var validationResult = _userCollectionValidator.ValidateRangeAsync(users);

        if (validationResult.Succeeded)
        {
            await _repositoryFactory.GetRepository<TimetableUser>().InsertAsync(users);
            return Result<ICollectionValidationResult<long>>.Create();
        }

        return validationResult;
    }

    public async Task<TimetableUser> FindAsync(long id)
    {
        return await _repositoryFactory
            .GetRepository<TimetableUser>()
            .FindAsync(new {Id = id});
    }

    public async Task<TimetableUser> FindAsync(object id)
    {
        return await _repositoryFactory
            .GetRepository<TimetableUser>()
            .FindAsync(id);
    }

    public async Task<TimetableUser> FindByEmailAsync(string email)
    {
        return await _repositoryFactory
            .GetRepository<TimetableUser>()
            .GetFirstOrDefaultAsync(
                predicate: u => u.Email == email);
    }

    public async Task UpdateAsync(TimetableUser user)
    {
        _repositoryFactory.GetRepository<TimetableUser>().Update(user);
    }

    public async Task UpdateRangeAsync(IEnumerable<TimetableUser> users)
    {
        _repositoryFactory.GetRepository<TimetableUser>().Update(users);
    }

    public async Task Remove(TimetableUser user)
    {
        _repositoryFactory.GetRepository<TimetableUser>().Delete(user);
    }

    public async Task RemoveById(object id)
    {
        _repositoryFactory.GetRepository<TimetableUser>().Delete(id);
    }

    public async Task RemoveRange(IEnumerable<TimetableUser> users)
    {
        _repositoryFactory.GetRepository<TimetableUser>().Delete(users);
    }

    public async Task RemoveRangeByIds(IEnumerable<object> ids)
    {
        _repositoryFactory.GetRepository<TimetableUser>().Delete(ids);
    }

    public async Task<TimetableUser> GetUserAsync(ClaimsPrincipal httpContextUser)
    {
        var idClaim = httpContextUser
            .FindFirst(x => x.Type == ClaimTypes.NameIdentifier);

        if (idClaim == null) return null;
        
        var id = long.Parse(idClaim.Value);

        return await FindAsync(id);
    }
}