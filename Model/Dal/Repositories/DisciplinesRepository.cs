using Repositories.EntityFramework;
using Timetable.Timetable.Model;

namespace Model.Dal.Repositories
{
    public class DisciplinesRepository: DbRepository<Discipline, long, TimetableDbContext>
    {
        public DisciplinesRepository(TimetableDbContext context) : base(context)
        {
        }
    }
}