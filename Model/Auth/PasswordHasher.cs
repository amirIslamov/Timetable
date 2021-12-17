using Microsoft.Extensions.Options;
using Model.Entities;
using Model.Users;

namespace Model.Auth;

public class BcryptPasswordHasher : IPasswordHasher
{
    public BcryptPasswordHasher(IOptions<PasswordHasherOptions> optionsAccessor = null)
    {
        var options = optionsAccessor?.Value ?? new PasswordHasherOptions();
        WorkFactor = options.WorkFactor;
    }

    protected int WorkFactor { get; set; }

    public string HashPassword(TimetableUser user, string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);
    }

    public PasswordVerificationResult VerifyHashedPassword(TimetableUser user, string hashedPassword,
        string providedPassword)
    {
        var isValid = BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);

        if (isValid && BCrypt.Net.BCrypt.PasswordNeedsRehash(hashedPassword, WorkFactor))
            return PasswordVerificationResult.SuccessRehashNeeded;

        return isValid ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
    }
}