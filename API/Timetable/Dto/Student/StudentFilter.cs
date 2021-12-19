using FilteringOrderingPagination.Models;
using FilteringOrderingPagination.Models.Specifications;

namespace API.Timetable.Dto.Student;

public class StudentFilter : IFilter<Model.Entities.Student>
{
    public StringPattern FirstName { get; set; }
    public StringPattern LastName { get; set; }
    public StringPattern Patronymic { get; set; }
    public StringPattern Email { get; set; }
    public ValuePropertyPattern<long> GroupId { get; set; }

    public Specification<Model.Entities.Student> ToSpecification()
    {
        var fnSpec = FirstName.AppliedTo<Model.Entities.Student>(s => s.User.FullName.FirstName);
        var lnSpec = LastName.AppliedTo<Model.Entities.Student>(s => s.User.FullName.LastName);
        var pnSpec = Patronymic.AppliedTo<Model.Entities.Student>(s => s.User.FullName.Patronymic);
        var emailSpec = Email.AppliedTo<Model.Entities.Student>(s => s.User.Email);
        var groupSpec = GroupId.AppliedTo<Model.Entities.Student>(s => s.GroupId);

        return fnSpec
            .And(lnSpec)
            .And(pnSpec)
            .And(emailSpec)
            .And(groupSpec);
    }
}