using API.Timetable.Dto.Classroom;
using API.Timetable.Dto.TimetableException;
using Arch.EntityFrameworkCore.UnitOfWork;
using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using FilteringOrderingPagination;
using FilteringOrderingPagination.Models;
using Microsoft.AspNetCore.Mvc;
using Model.Dal;
using Model.Entities;
using Model.Validation.Abstractions;

namespace API.Timetable.Controllers;

[ApiController]
[Route("api/v1/classrooms")]
public class ClassroomController: ControllerBase
{
    private readonly IUnitOfWork<TimetableDbContext> _unitOfWork;
    private readonly IValidator<Classroom> _validator;

    [HttpGet]
    public async Task<ActionResult<IPagedList<ListExceptionsResponse>>> GetClassrooms(
        FopRequest<Classroom, ClassroomFilter> request)
    {
        var pagedExceptions = await _unitOfWork
            .GetRepository<Classroom>()
            .GetPagedListAsync(
                c => ListClassroomsResponse.FromClassroom(c),
                request);

        return Ok(pagedExceptions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetClassroomResponse>> GetClassroom([FromRoute] long id)
    {
        var classroom = await _unitOfWork
            .GetRepository<Classroom>()
            .FindAsync(id);

        if (classroom == null) return NotFound();

        return GetClassroomResponse.FromClassroom(classroom);
    }

    [HttpPost]
    public async Task<IActionResult> CreateClassroom([FromBody] CreateClassroomRequest request)
    {
        var classroom = new Classroom()
        {
            Pavilion = request.Pavilion,
            ClassroomNumber = request.ClassroomNumber
        };

        var validationResult = await _validator.ValidateAsync(classroom);

        if (validationResult.Succeeded)
        {
            await _unitOfWork
                .GetRepository<Classroom>()
                .InsertAsync(classroom);

            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetClassroom),
                new {id = classroom.Id},
                GetClassroomResponse.FromClassroom(classroom));
        }

        return BadRequest(validationResult.Failure);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GetExceptionResponse>> UpdateClassroom(
        [FromRoute] long id, [FromBody] UpdateClassroomRequest request)
    {
        var classroom = await _unitOfWork
            .GetRepository<Classroom>()
            .FindAsync(id);

        if (classroom == null) return NotFound();

        classroom.Pavilion = request.Pavilion;
        classroom.ClassroomNumber = request.ClassroomNumber;

        var validationResult = await _validator.ValidateAsync(classroom);

        if (validationResult.Succeeded)
        {
            _unitOfWork
                .GetRepository<Classroom>()
                .Update(classroom);

            await _unitOfWork.SaveChangesAsync();

            return Ok(GetClassroomResponse.FromClassroom(classroom));
        }

        return BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveClassroom([FromRoute] long id)
    {
        var classroom = await _unitOfWork
            .GetRepository<Classroom>()
            .FindAsync(id);

        if (classroom == null) return NotFound();

        _unitOfWork
            .GetRepository<Classroom>()
            .Delete(classroom);

        await _unitOfWork.SaveChangesAsync();

        return Ok();
    }
}