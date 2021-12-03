using Repositories.EntityFramework;
using Timetable.Timetable.Model;

namespace Model.Dal.Repositories
{
    public class GroupsRepository: DbRepository<Group, long, TimetableDbContext>
    {
        public GroupsRepository(TimetableDbContext context) : base(context)
        {
        }
    }
}