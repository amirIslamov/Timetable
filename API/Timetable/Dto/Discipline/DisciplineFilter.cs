using FilteringOrderingPagination.Models;
using FilteringOrderingPagination.Models.Specifications;

namespace API.Timetable.Dto.Discipline;

public class DisciplineFilter : IFilter<Model.Entities.Discipline>
{
    public ValuePropertyPattern<long> SubjectId { get; set; }
    public EnumPattern<Model.Entities.Discipline.SemesterControlType> ControlType { get; set; }


    public Specification<Model.Entities.Discipline> ToSpecification()
    {
        var subjectIdSpec = SubjectId.AppliedTo<Model.Entities.Discipline>(d => d.SubjectId);
        var controlTypeSpec = ControlType.AppliedTo<Model.Entities.Discipline>(d => d.ControlType);

        return subjectIdSpec.And(controlTypeSpec);
    }
}