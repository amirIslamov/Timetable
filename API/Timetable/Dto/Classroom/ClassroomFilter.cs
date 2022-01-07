using FilteringOrderingPagination.Models;
using FilteringOrderingPagination.Models.Specifications;

namespace API.Timetable.Dto.Classroom;

public class ClassroomFilter: IFilter<Model.Entities.Classroom>
{
    public ValuePropertyPattern<int> ClassroomNumber { get; set; }
    public StringPattern Pavilion { get; set; }

    public Specification<Model.Entities.Classroom> ToSpecification()
    {
        var classroomNumSpec = ClassroomNumber
            .AppliedTo<Model.Entities.Classroom>(c => c.ClassroomNumber);
        var pavilionSpec = Pavilion
            .AppliedTo<Model.Entities.Classroom>(c => c.Pavilion);

        return classroomNumSpec.And(pavilionSpec);
    }
}