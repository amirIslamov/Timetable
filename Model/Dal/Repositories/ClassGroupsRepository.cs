using Repositories.EntityFramework;
using Timetable.Timetable.Model;

namespace Model.Dal.Repositories
{
    public class ClassGroupsRepository: DbRepository<ClassGroup, long, TimetableDbContext>
    {
        public ClassGroupsRepository(TimetableDbContext context) : base(context)
        {
        }
    }
}