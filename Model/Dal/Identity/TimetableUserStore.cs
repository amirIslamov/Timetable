using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model.Profile;

namespace Model.Dal.Identity
{
    public class TimetableUserStore: IUserPasswordStore<TimetableUser>, IUserEmailStore<TimetableUser>
    {
        public TimetableDbContext Context { get; set; }
        public bool AutoSaveChanges { get; set; } = true;

        public TimetableUserStore(TimetableDbContext context)
            => Context = context;
        
        protected Task SaveChanges(CancellationToken cancellationToken = default(CancellationToken))
        {
            return AutoSaveChanges ? Context.SaveChangesAsync(cancellationToken) : Task.CompletedTask;
        }

        public async Task<string> GetUserIdAsync(TimetableUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return user.Id.ToString();
        }

        public async Task<string> GetUserNameAsync(TimetableUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return user.Email;
        }

        public async Task SetUserNameAsync(TimetableUser user, string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException(nameof(email));
            }
            
            user.Email = email;
        }

        public async Task<string> GetNormalizedUserNameAsync(TimetableUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            
            return user.NormalizedEmail;
        }

        public async Task SetNormalizedUserNameAsync(TimetableUser user, string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException(nameof(email));
            }
            
            user.NormalizedEmail = email;
        }

        public async Task<IdentityResult> CreateAsync(TimetableUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            Context.Add(user);
            await SaveChanges(cancellationToken);
            
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(TimetableUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            Context.Attach(user);
            user.ConcurrencyStamp = Guid.NewGuid().ToString();
            Context.Update(user);
            
            try
            {
                await SaveChanges(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return IdentityResult.Failed(new IdentityError(){Code = "ConcurrencyFailure", Description = "Concurrency failure"});
            }
            
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(TimetableUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            Context.Remove(user);
            
            try
            {
                await SaveChanges(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return IdentityResult.Failed(new IdentityError(){Code = "ConcurrencyFailure", Description = "Concurrency failure"});
            }
            
            return IdentityResult.Success;
        }

        public async Task<TimetableUser> FindByIdAsync(string userId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var id = long.Parse(userId);
            return await Context.Users.FindAsync(new { id }, cancellationToken);
        }

        public async Task<TimetableUser> FindByNameAsync(string normaizedEmail, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Context.Users
                .Where(u => u.NormalizedEmail == normaizedEmail)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public IQueryable<TimetableUser> Users => Context.Users;

        public Task SetPasswordHashAsync(TimetableUser user, string passwordHash, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                throw new ArgumentException(nameof(passwordHash));
            }
            
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(TimetableUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(TimetableUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public async Task SetEmailAsync(TimetableUser user, string email, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException(nameof(email));
            }
            
            user.Email = email;
        }

        public async Task<string> GetEmailAsync(TimetableUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return user.Email;
        }

        public async Task<bool> GetEmailConfirmedAsync(TimetableUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            
            return user.EmailConfirmed;
        }

        public async Task SetEmailConfirmedAsync(TimetableUser user, bool confirmed, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.EmailConfirmed = confirmed;
        }

        public async Task<TimetableUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return await Context.Users
                .Where(u => u.NormalizedEmail == normalizedEmail)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<string> GetNormalizedEmailAsync(TimetableUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            
            return user.NormalizedEmail;
        }

        public async Task SetNormalizedEmailAsync(TimetableUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (string.IsNullOrWhiteSpace(normalizedEmail))
            {
                throw new ArgumentException(nameof(normalizedEmail));
            }
            
            user.NormalizedEmail = normalizedEmail;
        }
    }
}