using FilteringOrderingPagination.Models;
using FilteringOrderingPagination.Models.Specifications;

namespace API.Timetable.Dto.TimetableException;

public class ExceptionFilter : IFilter<Model.Entities.TimetableException>
{
    public ValuePropertyPattern<long> TimetableEntryId { get; set; }
    public DatePattern Date { get; set; }
    public ValuePropertyPattern<long> TeacherId { get; set; }
    public ValuePropertyPattern<long> GroupId { get; set; }

    public Specification<Model.Entities.TimetableException> ToSpecification()
    {
        var entrySpec = TimetableEntryId.AppliedTo<Model.Entities.TimetableException>(e => e.TimetableEntryId);
        var dateSpec = Date.AppliedTo<Model.Entities.TimetableException>(e => e.Date);
        var teacherSpec = TeacherId.AppliedTo<Model.Entities.TimetableException>(e => e.TeacherId);
        var groupSpec =
            GroupId.AppliedTo<Model.Entities.TimetableException>(e => e.TimetableEntry.TeacherLoad.Discipline.GroupId);

        return entrySpec
            .And(dateSpec)
            .And(teacherSpec)
            .And(groupSpec);
    }
}