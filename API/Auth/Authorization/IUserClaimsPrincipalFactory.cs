using System.Security.Claims;
using Model.Entities;

namespace API.Auth.Authorization;

public interface IUserClaimsPrincipalFactory
{
    Task<ClaimsPrincipal> CreateAsync(TimetableUser user);
}