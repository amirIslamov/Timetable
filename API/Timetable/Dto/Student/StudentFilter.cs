using FilteringOrderingPagination.Models;
using FilteringOrderingPagination.Models.Specifications;

namespace API.Timetable.Dto.Student;

public class StudentFilter : IFilter<Model.Entities.Student>
{
    public StringPattern FirstName { get; set; } = new StringPattern();
    public StringPattern LastName { get; set; } = new StringPattern();
    public StringPattern Patronymic { get; set; } = new StringPattern();
    public StringPattern Email { get; set; } = new StringPattern();

    public Specification<Model.Entities.Student> ToSpecification()
    {
        var fnSpec = FirstName.AppliedTo<Model.Entities.Student>(s => s.User.FullName.FirstName);
        var lnSpec = LastName.AppliedTo<Model.Entities.Student>(s => s.User.FullName.LastName);
        var pnSpec = Patronymic.AppliedTo<Model.Entities.Student>(s => s.User.FullName.Patronymic);
        var emailSpec = Email.AppliedTo<Model.Entities.Student>(s => s.User.Email);

        return fnSpec
            .And(lnSpec)
            .And(pnSpec)
            .And(emailSpec);
    }
}