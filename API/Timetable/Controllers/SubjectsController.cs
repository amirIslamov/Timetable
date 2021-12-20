using API.Timetable.Dto.Subject;
using Arch.EntityFrameworkCore.UnitOfWork;
using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using FilteringOrderingPagination;
using FilteringOrderingPagination.Models;
using Microsoft.AspNetCore.Mvc;
using Model.Dal;
using Model.Entities;
using Model.Validation.Abstractions;

namespace API.Timetable.Controllers;

[Route("api/v1/subjects")]
[ApiController]
public class SubjectsController : ControllerBase
{
    private readonly IValidator<Subject> _subjectValidator;
    private readonly IUnitOfWork<TimetableDbContext> _unitOfWork;

    public SubjectsController(IUnitOfWork<TimetableDbContext> unitOfWork, IValidator<Subject> subjectValidator)
    {
        _unitOfWork = unitOfWork;
        _subjectValidator = subjectValidator;
    }

    [HttpGet]
    public async Task<ActionResult<IPagedList<ListSubjectsResponse>>> GetSubjects(
        FopRequest<Subject, SubjectFilter> request)
    {
        var pagedSubjects = await _unitOfWork
            .GetRepository<Subject>()
            .GetPagedListAsync(
                s => ListSubjectsResponse.FromSubject(s),
                request);

        return Ok(pagedSubjects);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetSubjectResponse>> GetSubject(long id)
    {
        var subject = await _unitOfWork
            .GetRepository<Subject>()
            .FindAsync(id);

        if (subject == null) return NotFound();

        return GetSubjectResponse.FromSubject(subject);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubject(CreateSubjectRequest request)
    {
        var subject = new Subject
        {
            Code = request.Code,
            Name = request.Name
        };

        var validationResult = await _subjectValidator.ValidateAsync(subject);

        if (validationResult.Succeeded)
        {
            await _unitOfWork
                .GetRepository<Subject>()
                .InsertAsync(subject);

            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetSubject),
                new {id = subject.Id},
                GetSubjectResponse.FromSubject(subject));
        }

        return BadRequest(validationResult.Failure);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GetSubjectResponse>> UpdateSubject(long id, UpdateSubjectRequest request)
    {
        var subject = await _unitOfWork
            .GetRepository<Subject>()
            .FindAsync(id);

        if (subject == null) return NotFound();

        subject.Code = request.Code;
        subject.Name = request.Name;

        var validationResult = await _subjectValidator.ValidateAsync(subject);

        if (validationResult.Succeeded)
        {
            _unitOfWork
                .GetRepository<Subject>()
                .Update(subject);

            await _unitOfWork.SaveChangesAsync();

            return Ok(GetSubjectResponse.FromSubject(subject));
        }

        return BadRequest(validationResult.Failure);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveSubject(long id)
    {
        var subject = await _unitOfWork
            .GetRepository<Subject>()
            .FindAsync(id);

        if (subject == null) return NotFound();

        _unitOfWork.GetRepository<Subject>().Delete(subject);

        await _unitOfWork.SaveChangesAsync();

        return Ok();
    }
}