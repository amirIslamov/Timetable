using FilteringOrderingPagination.Models;
using FilteringOrderingPagination.Models.Specifications;
using Model.Entities;

namespace API.Timetable.Dto.Load;

public class LoadFilter : IFilter<TeacherLoad>
{
    public ValuePropertyPattern<long> TeacherId { get; set; }

    public Specification<TeacherLoad> ToSpecification()
    {
        var teacherSpec = TeacherId.AppliedTo<TeacherLoad>(l => l.TeacherId);

        return teacherSpec;
    }
}