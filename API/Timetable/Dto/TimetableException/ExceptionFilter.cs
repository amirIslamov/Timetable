using FilteringOrderingPagination.Models;
using FilteringOrderingPagination.Models.Specifications;

namespace API.Timetable.Dto.TimetableException;

public class ExceptionFilter : IFilter<Model.Entities.TimetableException>
{
    public DatePattern Date { get; set; } = new DatePattern();
    public ValuePropertyPattern<long> TeacherId { get; set; } = new ValuePropertyPattern<long>();
    public ValuePropertyPattern<long> GroupId { get; set; } = new ValuePropertyPattern<long>();

    public Specification<Model.Entities.TimetableException> ToSpecification()
    {
        var dateSpec = Date.AppliedTo<Model.Entities.TimetableException>(e => e.Date);
        var teacherSpec = TeacherId.AppliedTo<Model.Entities.TimetableException>(e => e.TeacherId);
        var groupSpec =
            GroupId.AppliedTo<Model.Entities.TimetableException>(e => e.TimetableEntry.TeacherLoad.Discipline.GroupId);

        return dateSpec
            .And(teacherSpec)
            .And(groupSpec);
    }
}