using Repositories.EntityFramework;
using Timetable.Auth.Model;

namespace Model.Dal.Repositories
{
    public class UsersRepository: DbRepository<TimetableUser, long, TimetableDbContext>
    {
        public UsersRepository(TimetableDbContext context) : base(context)
        {
        }
    }
}