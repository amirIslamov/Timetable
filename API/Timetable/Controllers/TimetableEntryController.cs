using API.Timetable.Dto.TimetableEntry;
using API.Timetable.Dto.TimetableException;
using Arch.EntityFrameworkCore.UnitOfWork;
using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using FilteringOrderingPagination;
using FilteringOrderingPagination.Models;
using FilteringOrderingPagination.Models.Specifications;
using Microsoft.AspNetCore.Mvc;
using Model.Dal;
using Model.Entities;
using Model.Validation.Abstractions;

namespace API.Timetable.Controllers;

[ApiController]
[Route("api/v1/entries")]
public class TimetableEntryController : ControllerBase
{
    private readonly IUnitOfWork<TimetableDbContext> _unitOfWork;
    private readonly IValidator<TimetableEntry> _validator;

    public TimetableEntryController(IUnitOfWork<TimetableDbContext> unitOfWork, IValidator<TimetableEntry> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    [HttpGet]
    public async Task<ActionResult<IPagedList<ListEntriesResponse>>> GetTimetableEntries(
        [FromQuery] FopRequest<TimetableEntry, EntryFilter> request)
    {
        var pagedEntries = await _unitOfWork
            .GetRepository<TimetableEntry>()
            .GetPagedListAsync(
                e => ListEntriesResponse.FromEntry(e),
                request);

        return Ok(pagedEntries);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetEntryResponse>> GetEntry([FromRoute] long id)
    {
        var entry = await _unitOfWork
            .GetRepository<TimetableEntry>()
            .FindAsync(id);

        if (entry == null) return NotFound();

        return GetEntryResponse.FromEntry(entry);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEntry([FromBody] CreateEntryRequest request)
    {
        var entry = new TimetableEntry
        {
            ClassroomId = request.ClassroomId,
            Link = request.Link,
            ClassNum = request.ClassNum,
            TeacherLoadId = request.TeacherLoadId,
            UpdatedAt = DateTime.UtcNow,
            WeekType = request.WeekType,
            DayOfWeek = request.DayOfWeek
        };

        var validationResult = await _validator.ValidateAsync(entry);

        if (validationResult.Succeeded)
        {
            await _unitOfWork.GetRepository<TimetableEntry>()
                .InsertAsync(entry);

            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetEntry),
                new {id = entry.Id},
                GetEntryResponse.FromEntry(entry));
        }

        return BadRequest(validationResult.Failure);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GetEntryResponse>> UpdateEntry([FromRoute] long id, [FromBody] UpdateEntryRequest request)
    {
        var entry = await _unitOfWork
            .GetRepository<TimetableEntry>()
            .FindAsync(id);

        if (entry == null) return NotFound();

        entry.ClassroomId = request.ClassroomId;
        entry.Link = request.Link;
        entry.ClassNum = request.ClassNum;
        entry.TeacherLoadId = request.TeacherLoadId;
        entry.UpdatedAt = DateTime.UtcNow;
        entry.WeekType = request.WeekType;
        entry.DayOfWeek = request.DayOfWeek;


        var validationResult = await _validator.ValidateAsync(entry);

        if (validationResult.Succeeded)
        {
            _unitOfWork
                .GetRepository<TimetableEntry>()
                .Insert(entry);

            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetEntry),
                new {id = entry.Id},
                entry);
        }

        return BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveEntry([FromRoute] long id)
    {
        var entry = await _unitOfWork
            .GetRepository<TimetableEntry>()
            .FindAsync(id);

        if (entry == null) return NotFound();

        _unitOfWork
            .GetRepository<TimetableEntry>()
            .Delete(entry);

        await _unitOfWork.SaveChangesAsync();

        return Ok();
    }
    
    [HttpGet("{entryId}/exceptions")]
    public async Task<ActionResult<IPagedList<ListExceptionsResponse>>> GetExceptions([FromRoute] long entryId,
        [FromQuery] FopRequest<TimetableException, ExceptionFilter> request)
    {
        var pagedExceptions = await _unitOfWork
            .GetRepository<TimetableException>()
            .GetPagedListAsync(
                selector: e => ListExceptionsResponse.FromException(e),
                specification: request.Filter.ToSpecification().And(e => e.TimetableEntryId == entryId),
                paging: request.Paging);

        return Ok(pagedExceptions);
    }
}