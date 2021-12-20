using API.Timetable.Dto.Discipline;
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
[Route("api/v1/disciplines")]
public class DisciplineController : ControllerBase
{
    private IUnitOfWork<TimetableDbContext> _unitOfWork;
    private IValidator<Discipline> _validator;

    [HttpGet]
    public async Task<ActionResult<IPagedList<ListDisciplinesResponse>>>
        GetDisciplines(FopRequest<Discipline, DisciplineFilter> request)
    {
        var pagedDisciplines = await _unitOfWork
            .GetRepository<Discipline>()
            .GetPagedListAsync(
                d => ListDisciplinesResponse.FromDiscipline(d),
                request);

        return Ok(pagedDisciplines);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetDisciplineResponse>> GetDiscipline(long id)
    {
        var discipline = await _unitOfWork
            .GetRepository<Discipline>()
            .FindAsync(id);

        if (discipline == null) return NotFound();

        return Ok(GetDisciplineResponse.FromDiscipline(discipline));
    }

    [HttpPost]
    public async Task<IActionResult> CreateDiscipline(CreateDisciplineRequest request)
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
    public async Task<IActionResult> UpdateDiscipline(long id, UpdateDisciplineRequest request)
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
    public async Task<IActionResult> DeleteDiscipline(long id)
    {
        var discipline = await _unitOfWork
            .GetRepository<Discipline>()
            .FindAsync(id);

        if (discipline == null) return NotFound();

        _unitOfWork.GetRepository<Discipline>().Delete(discipline);

        await _unitOfWork.SaveChangesAsync();

        return Ok();
    }
}