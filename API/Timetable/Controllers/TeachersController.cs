using API.Timetable.Dto.Teacher;
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

[Route("api/v1/teachers")]
[ApiController]
public class TeachersController : ControllerBase
{
    private readonly IUnitOfWork<TimetableDbContext> _unitOfWork;
    private readonly IValidator<Teacher> _validator;

    public TeachersController(IValidator<Teacher> validator, IUnitOfWork<TimetableDbContext> unitOfWork)
    {
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<IPagedList<ListTeachersResponse>>> GetTeachers(
        FopRequest<Teacher, TeacherFilter> request)
    {
        var pagedTeachers = await _unitOfWork
            .GetRepository<Teacher>()
            .GetPagedListAsync(
                s => ListTeachersResponse.FromTeacher(s),
                request);

        return Ok(pagedTeachers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetTeacherResponse>> GetTeacher(long id)
    {
        var teacher = await _unitOfWork
            .GetRepository<Teacher>()
            .GetFirstOrDefaultAsync(
                include: q => q.Include(s => s.User),
                predicate: s => s.Id == id);

        if (teacher == null) return NotFound();

        return GetTeacherResponse.FromTeacher(teacher);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTeacher(CreateTeacherRequest request)
    {
        var teacher = new Teacher
        {
            UserId =  request.UserId,
            Chair = request.Chair
        };

        var validationResult = await _validator.ValidateAsync(teacher);

        if (validationResult.Succeeded)
        {
            await _unitOfWork
                .GetRepository<Teacher>()
                .InsertAsync(teacher);

            var user = await _unitOfWork
                .GetRepository<TimetableUser>()
                .FindAsync(teacher.UserId);

            user.RoleSet.AddRole(Role.Teacher);

            _unitOfWork
                .GetRepository<TimetableUser>()
                .Update(user);

            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTeacher),
                new {id = teacher.Id},
                GetTeacherResponse.FromTeacher(teacher));
        }

        return BadRequest(validationResult.Failure);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GetTeacherResponse>> UpdateTeacher(UpdateTeacherRequest request, long id)
    {
        var teacher = await _unitOfWork
            .GetRepository<Teacher>()
            .GetFirstOrDefaultAsync(
                predicate: s => s.Id == id);

        teacher.Chair = request.Chair;

        var validationResult = await _validator.ValidateAsync(teacher);

        if (validationResult.Succeeded)
        {
            _unitOfWork
                .GetRepository<Teacher>()
                .Update(teacher);

            await _unitOfWork.SaveChangesAsync();

            return GetTeacherResponse.FromTeacher(teacher);
        }

        return BadRequest(validationResult.Failure);
    }
}