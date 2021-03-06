using FilteringOrderingPagination.Models;
using FilteringOrderingPagination.Models.Specifications;

namespace API.Timetable.Dto.Subject;

public class SubjectFilter : IFilter<Model.Entities.Subject>
{
    public StringPattern Name { get; set; } = new StringPattern();
    public StringPattern Code { get; set; } = new StringPattern();

    public Specification<Model.Entities.Subject> ToSpecification()
    {
        var nameSpec = Name.AppliedTo<Model.Entities.Subject>(s => s.Name);
        var codeSpec = Code.AppliedTo<Model.Entities.Subject>(s => s.Code);

        return nameSpec.And(codeSpec);
    }
}