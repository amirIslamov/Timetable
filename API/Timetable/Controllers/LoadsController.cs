using API.Timetable.Dto.Load;
using API.Timetable.Dto.TimetableEntry;
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
[Route("api/v1/loads")]
public class LoadsController : ControllerBase
{
    private readonly IUnitOfWork<TimetableDbContext> _unitOfWork;
    private readonly IValidator<TeacherLoad> _validator;

    public LoadsController(IValidator<TeacherLoad> validator, IUnitOfWork<TimetableDbContext> unitOfWork)
    {
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<IPagedList<ListLoadsResponse>>> GetLoads([FromQuery] FopRequest<TeacherLoad, LoadFilter> request)
    {
        var pagedLoads = await _unitOfWork
            .GetRepository<TeacherLoad>()
            .GetPagedListAsync(
                l => ListLoadsResponse.FromLoad(l),
                request);

        return Ok(pagedLoads);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetLoadResponse>> GetLoad([FromRoute] long id)
    {
        var load = await _unitOfWork
            .GetRepository<TeacherLoad>()
            .FindAsync(id);

        if (load == null) return NotFound();

        return Ok(GetLoadResponse.FromLoad(load));
    }

    [HttpPost]
    public async Task<IActionResult> CreateLoad([FromBody] CreateLoadRequest request)
    {
        var load = new TeacherLoad
        {
            DisciplineId = request.DisciplineId,
            TeacherId = request.TeacherId,
            Type = request.Type,
            TotalHours = request.TotalHours
        };

        var validationResult = await _validator.ValidateAsync(load);

        if (validationResult.Succeeded)
        {
            await _unitOfWork
                .GetRepository<TeacherLoad>()
                .InsertAsync(load);

            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetLoad),
                new {id = load.Id},
                GetLoadResponse.FromLoad(load));
        }

        return BadRequest(validationResult.Failure);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GetLoadResponse>> UpdateLoad([FromRoute] long id, [FromBody] UpdateLoadRequest request)
    {
        var load = await _unitOfWork
            .GetRepository<TeacherLoad>()
            .FindAsync(id);

        if (load == null) return NotFound();

        load.Type = request.Type;
        load.TeacherId = request.TeacherId;
        load.TotalHours = request.TotalHours;

        var validationResult = await _validator.ValidateAsync(load);

        if (validationResult.Succeeded)
        {
            _unitOfWork
                .GetRepository<TeacherLoad>()
                .Update(load);

            await _unitOfWork.SaveChangesAsync();

            return Ok(GetLoadResponse.FromLoad(load));
        }

        return BadRequest(validationResult.Failure);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveLoad([FromRoute] long id)
    {
        var load = await _unitOfWork
            .GetRepository<TeacherLoad>()
            .FindAsync(id);

        if (load == null) return NotFound();

        _unitOfWork
            .GetRepository<TeacherLoad>()
            .Delete(load);

        await _unitOfWork.SaveChangesAsync();

        return Ok();
    }
    
    [HttpGet("{loadId}/entries")]
    public async Task<ActionResult<IPagedList<ListEntriesResponse>>> GetTimetableEntries([FromRoute] long loadId,
        [FromQuery] FopRequest<TimetableEntry, EntryFilter> request)
    {
        var pagedEntries = await _unitOfWork
            .GetRepository<TimetableEntry>()
            .GetPagedListAsync(
                selector: e => ListEntriesResponse.FromEntry(e),
                specification: request.Filter.ToSpecification().And(e => e.TeacherLoadId == loadId),
                paging: request.Paging);

        return Ok(pagedEntries);
    }
}