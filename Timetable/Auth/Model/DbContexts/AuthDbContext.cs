using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Timetable.Auth.Model.DbContexts
{
    public class AuthDbContext: IdentityDbContext<TimetableUser, TimetableRole, long>
    {
    }
}