using FilteringOrderingPagination.Models;
using FilteringOrderingPagination.Models.Specifications;
using Model.Entities;

namespace API.Timetable.Dto.TimetableEntry;

public class EntryFilter : IFilter<Model.Entities.TimetableEntry>
{
    public EnumPattern<DayOfWeek> DayOfWeek { get; set; }
    public EnumPattern<WeekType> WeekType { get; set; }
    public ValuePropertyPattern<long> TeacherLoadId { get; set; }
    public ValuePropertyPattern<long> GroupId { get; set; }
    public ValuePropertyPattern<long> TeacherId { get; set; }

    public Specification<Model.Entities.TimetableEntry> ToSpecification()
    {
        var daySpec = DayOfWeek.AppliedTo<Model.Entities.TimetableEntry>(e => e.DayOfWeek);
        var weekSpec = WeekType.AppliedTo<Model.Entities.TimetableEntry>(e => e.WeekType);
        var loadSpec = TeacherLoadId.AppliedTo<Model.Entities.TimetableEntry>(e => e.TeacherLoadId);
        var teacherSpec = TeacherId.AppliedTo<Model.Entities.TimetableEntry>(e => e.TeacherLoad.TeacherId);
        var groupSpec = GroupId.AppliedTo<Model.Entities.TimetableEntry>(e => e.TeacherLoad.Discipline.GroupId);

        return new AndSpecification<Model.Entities.TimetableEntry>(daySpec, weekSpec, loadSpec, teacherSpec, groupSpec);
    }
}