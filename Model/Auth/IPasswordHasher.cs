using Model.Entities;

namespace Model.Users;

public interface IPasswordHasher
{
    string HashPassword(TimetableUser user, string password);
    PasswordVerificationResult VerifyHashedPassword(TimetableUser user, string hashedPassword, string providedPassword);
}

public enum PasswordVerificationResult
{
    Failed = 0,
    Success = 1,
    SuccessRehashNeeded = 2
}