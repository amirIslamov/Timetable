using API.Timetable.Dto.TimetableEntry;
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
public class TimetableController : ControllerBase
{
    private readonly IUnitOfWork<TimetableDbContext> _unitOfWork;
    private readonly IValidator<TimetableEntry> _validator;

    public TimetableController(IUnitOfWork<TimetableDbContext> unitOfWork, IValidator<TimetableEntry> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    [HttpGet("entries")]
    public async Task<ActionResult<IPagedList<ListEntriesResponse>>> GetTimetableEntries(
        FopRequest<TimetableEntry, EntryFilter> request)
    {
        var pagedEntries = await _unitOfWork
            .GetRepository<TimetableEntry>()
            .GetPagedListAsync(
                e => ListEntriesResponse.FromEntry(e),
                request);

        return Ok(pagedEntries);
    }

    [HttpGet("entries/{id}")]
    public async Task<ActionResult<GetEntryResponse>> GetEntry(long id)
    {
        var entry = await _unitOfWork
            .GetRepository<TimetableEntry>()
            .FindAsync(new {Id = id});

        if (entry == null) return NotFound();

        return GetEntryResponse.FromEntry(entry);
    }

    [HttpPost("entries")]
    public async Task<IActionResult> CreateEntry(CreateEntryRequest request)
    {
        var entry = new TimetableEntry
        {
            Classroom = request.Classroom,
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

    [HttpPut("entries/{id}")]
    public async Task<ActionResult<GetEntryResponse>> UpdateEntry(long id, UpdateEntryRequest request)
    {
        var entry = await _unitOfWork
            .GetRepository<TimetableEntry>()
            .FindAsync(new {Id = id});

        if (entry == null) return NotFound();

        entry.Classroom = request.Classroom;
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

    [HttpDelete("entries/{id}")]
    public async Task<IActionResult> RemoveEntry(long id)
    {
        var entry = await _unitOfWork
            .GetRepository<TimetableEntry>()
            .FindAsync(new {Id = id});

        if (entry == null) return NotFound();

        _unitOfWork
            .GetRepository<TimetableEntry>()
            .Delete(entry);

        await _unitOfWork.SaveChangesAsync();

        return Ok();
    }
}