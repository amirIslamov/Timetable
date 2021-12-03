using Model.Timetable;
using Repositories.EntityFramework;

namespace Model.Dal.Repositories
{
    public class StudentsRepository: DbRepository<Student, long, TimetableDbContext>
    {
        public StudentsRepository(TimetableDbContext context) : base(context)
        {
        }
    }
}