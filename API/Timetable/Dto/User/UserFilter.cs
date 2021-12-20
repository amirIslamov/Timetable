using FilteringOrderingPagination.Models;
using FilteringOrderingPagination.Models.Specifications;
using Model.Entities;

namespace API.Timetable.Dto.User;

public class UserFilter : IFilter<TimetableUser>
{
    public StringPattern FirstName { get; set; } = new StringPattern();
    public StringPattern LastName { get; set; } = new StringPattern();
    public StringPattern Patronymic { get; set; } = new StringPattern();
    public StringPattern Email { get; set; } = new StringPattern();

    public Specification<TimetableUser> ToSpecification()
    {
        var fnSpec = FirstName.AppliedTo<TimetableUser>(u => u.FullName.FirstName);
        var lnSpec = LastName.AppliedTo<TimetableUser>(u => u.FullName.LastName);
        var pnSpec = Patronymic.AppliedTo<TimetableUser>(u => u.FullName.Patronymic);
        var emailSpec = Email.AppliedTo<TimetableUser>(u => u.Email);

        return fnSpec
            .And(lnSpec)
            .And(pnSpec)
            .And(emailSpec);
    }
}