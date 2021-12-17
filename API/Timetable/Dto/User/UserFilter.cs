using FilteringOrderingPagination.Models;
using FilteringOrderingPagination.Models.Specifications;
using Model.Entities;

namespace API.Timetable.Dto.User;

public class UserFilter : IFilter<TimetableUser>
{
    public StringPattern FirstName { get; set; }
    public StringPattern LastName { get; set; }
    public StringPattern Patronymic { get; set; }
    public StringPattern Email { get; set; }

    public Specification<TimetableUser> ToSpecification()
    {
        var fnSpec = FirstName.AppliedTo<TimetableUser>(u => u.FullName.FirstName);
        var lnSpec = LastName.AppliedTo<TimetableUser>(u => u.FullName.LastName);
        var pnSpec = Patronymic.AppliedTo<TimetableUser>(u => u.FullName.Patronymic);
        var emailSpec = Email.AppliedTo<TimetableUser>(u => u.Email);

        return new AndSpecification<TimetableUser>(
            fnSpec, lnSpec, pnSpec, emailSpec
        );
    }
}