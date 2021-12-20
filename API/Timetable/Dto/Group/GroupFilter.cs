using FilteringOrderingPagination.Models;
using FilteringOrderingPagination.Models.Specifications;

namespace API.Timetable.Dto.Group;

public class GroupFilter : IFilter<Model.Entities.Group>
{
    public ValuePropertyPattern<long> CuratorId { get; set; } = new ValuePropertyPattern<long>();
    public StringPattern Name { get; set; } = new StringPattern();
    public StringPattern ShortName { get; set; } = new StringPattern();

    public Specification<Model.Entities.Group> ToSpecification()
    {
        var curatorIdSpec = CuratorId.AppliedTo<Model.Entities.Group>(d => d.CuratorId);
        var nameSpec = Name.AppliedTo<Model.Entities.Group>(d => d.Name);
        var shortNameSpec = ShortName.AppliedTo<Model.Entities.Group>(d => d.ShortName);

        return curatorIdSpec.And(nameSpec).And(shortNameSpec);
    }
}