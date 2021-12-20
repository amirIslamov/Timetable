using FilteringOrderingPagination.Models;
using FilteringOrderingPagination.Models.Specifications;

namespace API.Timetable.Dto.Teacher;

public class TeacherFilter : IFilter<Model.Entities.Teacher>
{
    public StringPattern FirstName { get; set; } = new StringPattern();
    public StringPattern LastName { get; set; } = new StringPattern();
    public StringPattern Patronymic { get; set; } = new StringPattern();
    public StringPattern Email { get; set; } = new StringPattern();
    public StringPattern Chair { get; set; } = new StringPattern();

    public Specification<Model.Entities.Teacher> ToSpecification()
    {
        var fnSpec = FirstName.AppliedTo<Model.Entities.Teacher>(s => s.User.FullName.FirstName);
        var lnSpec = LastName.AppliedTo<Model.Entities.Teacher>(s => s.User.FullName.LastName);
        var pnSpec = Patronymic.AppliedTo<Model.Entities.Teacher>(s => s.User.FullName.Patronymic);
        var emailSpec = Email.AppliedTo<Model.Entities.Teacher>(s => s.User.Email);
        var chairSpec = Chair.AppliedTo<Model.Entities.Teacher>(s => s.Chair);

        return fnSpec
            .And(lnSpec)
            .And(pnSpec)
            .And(emailSpec)
            .And(chairSpec);
    }
}