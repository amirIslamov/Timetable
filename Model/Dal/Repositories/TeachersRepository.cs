using Repositories.EntityFramework;
using Timetable.Timetable.Model;

namespace Model.Dal.Repositories
{
    public class TeachersRepository: DbRepository<Teacher, long, TimetableDbContext>
    {
        public TeachersRepository(TimetableDbContext context) : base(context)
        {
        }
    }
}