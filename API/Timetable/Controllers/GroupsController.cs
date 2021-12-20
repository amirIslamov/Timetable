using API.Timetable.Dto.Discipline;
using API.Timetable.Dto.Group;
using API.Timetable.Dto.Student;
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

[Route("api/v1/groups")]
[ApiController]
public class GroupsController : ControllerBase
{
    private readonly IValidator<Group> _groupValidator;
    private readonly IUnitOfWork<TimetableDbContext> _unitOfWork;

    public GroupsController(IUnitOfWork<TimetableDbContext> unitOfWork, IValidator<Group> groupValidator)
    {
        _unitOfWork = unitOfWork;
        _groupValidator = groupValidator;
    }

    [HttpGet]
    public async Task<ActionResult<IPagedList<ListGroupsResponse>>> GetGroups([FromQuery] FopRequest<Group, GroupFilter> request)
    {
        var pagedGroups = await _unitOfWork
            .GetRepository<Group>()
            .GetPagedListAsync(
                s => ListGroupsResponse.FromGroup(s),
                request);

        return Ok(pagedGroups);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetGroupResponse>> GetGroup([FromRoute] long id)
    {
        var group = await _unitOfWork
            .GetRepository<Group>()
            .FindAsync(id);

        if (group == null) return NotFound();

        return GetGroupResponse.FromGroup(group);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGroup([FromBody] CreateGroupRequest request)
    {
        var group = new Group
        {
            Name = request.Name,
            ShortName = request.ShortName,
            AdmissionYear = request.AdmissionYear,
            CuratorId = request.CuratorId
        };

        var validationResult = await _groupValidator.ValidateAsync(group);

        if (validationResult.Succeeded)
        {
            await _unitOfWork
                .GetRepository<Group>()
                .InsertAsync(group);

            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetGroup),
                new {id = group.Id},
                GetGroupResponse.FromGroup(group));
        }

        return BadRequest(validationResult.Failure);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GetGroupResponse>> UpdateGroup([FromBody] UpdateGroupRequest request, [FromRoute] long id)
    {
        var group = await _unitOfWork
            .GetRepository<Group>()
            .FindAsync(id);

        if (group == null) return NotFound();

        group.Name = request.Name;
        group.ShortName = request.ShortName;
        group.AdmissionYear = request.AdmissionYear;
        group.CuratorId = request.CuratorId;

        var validationResult = await _groupValidator.ValidateAsync(group);

        if (validationResult.Succeeded)
        {
            _unitOfWork
                .GetRepository<Group>()
                .Update(group);

            await _unitOfWork.SaveChangesAsync();

            return Ok(GetGroupResponse.FromGroup(group));
        }

        return BadRequest(validationResult.Failure);
    }
    
    [HttpGet("{groupId}/students")]
    public async Task<ActionResult<IPagedList<ListStudentsResponse>>> GetStudents([FromRoute] long groupId,
        [FromQuery] FopRequest<Student, StudentFilter> request)
    {
        var pagedStudents = await _unitOfWork
            .GetRepository<Student>()
            .GetPagedListAsync(
                selector: s => ListStudentsResponse.FromStudent(s),
                specification: request.Filter.ToSpecification().And(s => s.GroupId == groupId),
                paging: request.Paging);

        return Ok(pagedStudents);
    }
    
    [HttpGet("{groupId}/disciplines")]
    public async Task<ActionResult<IPagedList<ListDisciplinesResponse>>>
        GetDisciplines([FromRoute] long groupId, [FromQuery] FopRequest<Discipline, DisciplineFilter> request)
    {
        var pagedDisciplines = await _unitOfWork
            .GetRepository<Discipline>()
            .GetPagedListAsync(
                selector: d => ListDisciplinesResponse.FromDiscipline(d),
                specification: request.Filter.ToSpecification().And(l => l.GroupId == groupId),
                paging: request.Paging);

        return Ok(pagedDisciplines);
    }
}