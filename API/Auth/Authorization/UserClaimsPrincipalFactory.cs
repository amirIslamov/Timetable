using System.Security.Claims;
using Model.Entities;

namespace API.Auth.Authorization;

internal class UserClaimsPrincipalFactory : IUserClaimsPrincipalFactory
{
    public virtual async Task<ClaimsPrincipal> CreateAsync(TimetableUser user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        var id = await GenerateClaimsAsync(user);
        return new ClaimsPrincipal(id);
    }

    protected virtual async Task<ClaimsIdentity> GenerateClaimsAsync(TimetableUser user)
    {
        var userId = user.Id;
        var id = new ClaimsIdentity("Identity.Application");

        id.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId.ToString()));

        var roles = user.RoleSet.Roles.Select(r => r.ToString());
        foreach (var roleName in roles) id.AddClaim(new Claim(ClaimTypes.Role, roleName));

        return id;
    }
}