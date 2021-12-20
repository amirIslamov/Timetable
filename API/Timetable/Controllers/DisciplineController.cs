using API.Timetable.Dto.Discipline;
using API.Timetable.Dto.Load;
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
[Route("api/v1/disciplines")]
public class DisciplineController : ControllerBase
{
    private readonly IUnitOfWork<TimetableDbContext> _unitOfWork;
    private readonly IValidator<Discipline> _validator;

    public DisciplineController(IUnitOfWork<TimetableDbContext> unitOfWork, IValidator<Discipline> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    [HttpGet]
    public async Task<ActionResult<IPagedList<ListDisciplinesResponse>>>
        GetDisciplines([FromQuery] FopRequest<Discipline, DisciplineFilter> request)
    {
        var pagedDisciplines = await _unitOfWork
            .GetRepository<Discipline>()
            .GetPagedListAsync(
                d => ListDisciplinesResponse.FromDiscipline(d),
                request);

        return Ok(pagedDisciplines);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetDisciplineResponse>> GetDiscipline([FromRoute] long id)
    {
        var discipline = await _unitOfWork
            .GetRepository<Discipline>()
            .FindAsync(id);

        if (discipline == null) return NotFound();

        return Ok(GetDisciplineResponse.FromDiscipline(discipline));
    }

    [HttpPost]
    public async Task<IActionResult> CreateDiscipline([FromBody] CreateDisciplineRequest request)
    {
        var discipline = new Discipline
        {
            ControlType = request.ControlType,
            GroupId = request.GroupId,
            SubjectId = request.SubjectId,
            SemesterNumber = request.SemesterNumber,
            ClassroomWorkHours = request.ClassroomWorkHours,
            IndependentWorkHours = request.IndependentWorkHours
        };

        var validationResult = await _validator.ValidateAsync(discipline);

        if (validationResult.Succeeded)
        {
            await _unitOfWork
                .GetRepository<Discipline>()
                .InsertAsync(discipline);

            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetDiscipline),
                new {id = discipline.Id},
                GetDisciplineResponse.FromDiscipline(discipline));
        }

        return BadRequest(validationResult.Failure);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDiscipline([FromRoute] long id, [FromBody] UpdateDisciplineRequest request)
    {
        var discipline = await _unitOfWork
            .GetRepository<Discipline>()
            .FindAsync(id);

        discipline.ControlType = request.ControlType;
        discipline.SubjectId = request.SubjectId;
        discipline.SemesterNumber = request.SemesterNumber;
        discipline.ClassroomWorkHours = request.ClassroomWorkHours;
        discipline.IndependentWorkHours = request.IndependentWorkHours;

        var validationResult = await _validator.ValidateAsync(discipline);

        if (validationResult.Succeeded)
        {
            _unitOfWork
                .GetRepository<Discipline>()
                .Update(discipline);

            await _unitOfWork.SaveChangesAsync();

            return Ok(GetDisciplineResponse.FromDiscipline(discipline));
        }

        return BadRequest(validationResult.Failure);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDiscipline([FromRoute] long id)
    {
        var discipline = await _unitOfWork
            .GetRepository<Discipline>()
            .FindAsync(id);

        if (discipline == null) return NotFound();

        _unitOfWork.GetRepository<Discipline>().Delete(discipline);

        await _unitOfWork.SaveChangesAsync();

        return Ok();
    }
    
    [HttpGet("{disciplineId}/loads")]
    public async Task<ActionResult<IPagedList<ListLoadsResponse>>> GetLoads([FromRoute] long disciplineId,
        [FromQuery] FopRequest<TeacherLoad, LoadFilter> request)
    {
        var pagedLoads = await _unitOfWork
            .GetRepository<TeacherLoad>()
            .GetPagedListAsync(
                selector: l => ListLoadsResponse.FromLoad(l),
                specification: request.Filter.ToSpecification().And(l => l.DisciplineId == disciplineId),
                paging: request.Paging);

        return Ok(pagedLoads);
    }
}