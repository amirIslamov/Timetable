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
[Route("api/v1/timetable")]
public class TimeTableExceptionController : ControllerBase
{
    private readonly IUnitOfWork<TimetableDbContext> _unitOfWork;
    private readonly IValidator<TimetableException> _validator;

    [HttpGet("exceptions")]
    public async Task<ActionResult<IPagedList<ListExceptionsResponse>>> GetExceptions(
        FopRequest<TimetableException, ExceptionFilter> request)
    {
        var pagedExceptions = await _unitOfWork
            .GetRepository<TimetableException>()
            .GetPagedListAsync(
                e => ListExceptionsResponse.FromException(e),
                request);

        return Ok(pagedExceptions);
    }

    [HttpGet("exceptions/{id}")]
    public async Task<ActionResult<GetExceptionResponse>> GetException(long id)
    {
        var exception = await _unitOfWork
            .GetRepository<TimetableException>()
            .FindAsync(new {Id = id});

        if (exception == null) return NotFound();

        return GetExceptionResponse.FromException(exception);
    }

    [HttpPost("exceptions")]
    public async Task<IActionResult> CreateException(CreateExceptionRequest request)
    {
        var exception = new TimetableException
        {
            Classroom = request.Classroom,
            Date = request.Date,
            Link = request.Link,
            ClassNum = request.ClassNum,
            UpdatedAt = DateTime.UtcNow,
            TimetableEntryId = request.TimetableEntryId,
            TeacherId = request.TeacherId
        };

        var validationResult = await _validator.ValidateAsync(exception);

        if (validationResult.Succeeded)
        {
            await _unitOfWork
                .GetRepository<TimetableException>()
                .InsertAsync(exception);

            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetException),
                new {id = exception.Id},
                GetExceptionResponse.FromException(exception));
        }

        return BadRequest(validationResult.Failure);
    }

    [HttpPut("exceptions/{id}")]
    public async Task<ActionResult<GetExceptionResponse>> UpdateException(long id, UpdateExceptionRequest request)
    {
        var exception = await _unitOfWork
            .GetRepository<TimetableException>()
            .FindAsync(new {Id = id});

        if (exception == null) return NotFound();

        exception.Classroom = request.Classroom;
        exception.Link = request.Link;
        exception.ClassNum = request.ClassNum;
        exception.TimetableEntryId = request.TimetableEntryId;
        exception.UpdatedAt = DateTime.UtcNow;
        exception.Date = request.Date;
        exception.TeacherId = request.TeacherId;

        var validationResult = await _validator.ValidateAsync(exception);

        if (validationResult.Succeeded)
        {
            _unitOfWork
                .GetRepository<TimetableException>()
                .Update(exception);

            await _unitOfWork.SaveChangesAsync();

            return Ok(GetExceptionResponse.FromException(exception));
        }

        return BadRequest();
    }

    [HttpDelete("exceptions/{id}")]
    public async Task<IActionResult> RemoveException(long id)
    {
        var exception = await _unitOfWork
            .GetRepository<TimetableException>()
            .FindAsync(new {Id = id});

        if (exception == null) return NotFound();

        _unitOfWork
            .GetRepository<TimetableException>()
            .Delete(exception);

        await _unitOfWork.SaveChangesAsync();

        return Ok();
    }
}