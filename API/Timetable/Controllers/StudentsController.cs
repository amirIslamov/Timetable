using API.Timetable.Dto.Student;
using Arch.EntityFrameworkCore.UnitOfWork;
using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using FilteringOrderingPagination;
using FilteringOrderingPagination.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Dal;
using Model.Entities;
using Model.Profile.Roles;
using Model.Validation.Abstractions;

namespace API.Timetable.Controllers;

[Route("api/v1/students")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly IUnitOfWork<TimetableDbContext> _unitOfWork;
    private readonly IValidator<Student> _validator;

    public StudentsController(IValidator<Student> validator, IUnitOfWork<TimetableDbContext> unitOfWork)
    {
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<IPagedList<ListStudentsResponse>>> GetStudents(
        [FromQuery] FopRequest<Student, StudentFilter> request)
    {
        var pagedStudents = await _unitOfWork
            .GetRepository<Student>()
            .GetPagedListAsync(
                s => ListStudentsResponse.FromStudent(s),
                request);

        return Ok(pagedStudents);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetStudentResponse>> GetStudent([FromRoute] long id)
    {
        var student = await _unitOfWork
            .GetRepository<Student>()
            .GetFirstOrDefaultAsync(
                include: q => q.Include(s => s.User),
                predicate: s => s.Id == id);

        if (student == null) return NotFound();

        return GetStudentResponse.FromStudent(student);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudent([FromBody] CreateStudentRequest request)
    {
        var student = new Student
        {
            GroupId = request.GroupId,
            FatherContacts = request.FatherContacts,
            MotherContacts = request.MotherContacts,
            UserId = request.UserId,
        };

        var validationResult = await _validator.ValidateAsync(student);

        if (validationResult.Succeeded)
        {
            await _unitOfWork
                .GetRepository<Student>()
                .InsertAsync(student);

            var user = await _unitOfWork
                .GetRepository<TimetableUser>()
                .FindAsync(student.UserId);

            user.RoleSet.AddRole(Role.Student);

            _unitOfWork
                .GetRepository<TimetableUser>()
                .Update(user);

            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent),
                new {id = student.Id},
                GetStudentResponse.FromStudent(student));
        }

        return BadRequest(validationResult.Failure);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GetStudentResponse>> UpdateStudent([FromBody] UpdateStudentRequest request,[FromRoute] long id)
    {
        var student = await _unitOfWork
            .GetRepository<Student>()
            .GetFirstOrDefaultAsync(
                predicate: s => s.Id == id);

        if (student == null)
        {
            return NotFound();
        }

        student.FatherContacts = request.FatherContacts;
        student.MotherContacts = request.MotherContacts;
        student.GroupId = request.GroupId;

        var validationResult = await _validator.ValidateAsync(student);

        if (validationResult.Succeeded)
        {
            _unitOfWork
                .GetRepository<Student>()
                .Update(student);

            await _unitOfWork.SaveChangesAsync();

            return GetStudentResponse.FromStudent(student);
        }

        return BadRequest(validationResult.Failure);
    }
}