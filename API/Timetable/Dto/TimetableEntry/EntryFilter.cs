using FilteringOrderingPagination.Models;
using FilteringOrderingPagination.Models.Specifications;
using Model.Entities;

namespace API.Timetable.Dto.TimetableEntry;

public class EntryFilter : IFilter<Model.Entities.TimetableEntry>
{
    public EnumPattern<DayOfWeek> DayOfWeek { get; set; } = new EnumPattern<DayOfWeek>();
    public EnumPattern<WeekType> WeekType { get; set; } = new EnumPattern<WeekType>();
    public ValuePropertyPattern<long> GroupId { get; set; } = new ValuePropertyPattern<long>();
    public ValuePropertyPattern<long> TeacherId { get; set; } = new ValuePropertyPattern<long>();

    public Specification<Model.Entities.TimetableEntry> ToSpecification()
    {
        var daySpec = DayOfWeek.AppliedTo<Model.Entities.TimetableEntry>(e => e.DayOfWeek);
        var weekSpec = WeekType.AppliedTo<Model.Entities.TimetableEntry>(e => e.WeekType);
        var teacherSpec = TeacherId.AppliedTo<Model.Entities.TimetableEntry>(e => e.TeacherLoad.TeacherId);
        var groupSpec = GroupId.AppliedTo<Model.Entities.TimetableEntry>(e => e.TeacherLoad.Discipline.GroupId);

        return daySpec
            .And(weekSpec)
            .And(teacherSpec)
            .And(groupSpec);
    }
}