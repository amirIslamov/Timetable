using Repositories.EntityFramework;
using Timetable.Timetable.Model;

namespace Model.Dal.Repositories
{
    public class SubjectsRepository: DbRepository<Subject, long, TimetableDbContext>
    {
        public SubjectsRepository(TimetableDbContext context) : base(context)
        {
        }
    }
}