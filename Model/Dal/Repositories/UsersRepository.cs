using Model.Profile;
using Repositories.EntityFramework;

namespace Model.Dal.Repositories
{
    public class UsersRepository: DbRepository<TimetableUser, long, TimetableDbContext>
    {
        public UsersRepository(TimetableDbContext context) : base(context)
        {
        }
    }
}