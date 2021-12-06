using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Model.Profile.Roles;
using Model.Profile;

namespace API.Auth.Authorization.Identity
{
    public class TimetableUserClaimsPrincipalFactory: IUserClaimsPrincipalFactory<TimetableUser>
    {
        public TimetableUserClaimsPrincipalFactory(
            UserManager<TimetableUser> userManager,
            IOptions<IdentityOptions> optionsAccessor)
        {
            if (optionsAccessor == null || optionsAccessor.Value == null)
            {
                throw new ArgumentNullException(nameof(optionsAccessor));
            }
            
            UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            Options = optionsAccessor.Value;
        }
        
        public UserManager<TimetableUser> UserManager { get; private set; }

        public IdentityOptions Options { get; private set; }

        public virtual async Task<ClaimsPrincipal> CreateAsync(TimetableUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var id = await GenerateClaimsAsync(user);
            return new ClaimsPrincipal(id);
        }
        
        protected virtual async Task<ClaimsIdentity> GenerateClaimsAsync(TimetableUser user)
        {
            var userId = await UserManager.GetUserIdAsync(user);
            var userName = await UserManager.GetUserNameAsync(user);
            var id = new ClaimsIdentity("Identity.Application");
            
            id.AddClaim(new Claim(Options.ClaimsIdentity.UserIdClaimType, userId));
            id.AddClaim(new Claim(Options.ClaimsIdentity.UserNameClaimType, userName));
            
            var roles = user.RoleSet.Roles.Select(r => r.ToString());
            foreach (var roleName in roles)
            {
                id.AddClaim(new Claim(Options.ClaimsIdentity.RoleClaimType, roleName));
            }
            
            return id;
        }
    }
}