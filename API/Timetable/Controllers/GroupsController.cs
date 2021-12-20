using API.Timetable.Dto.Group;
using Arch.EntityFrameworkCore.UnitOfWork;
using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using FilteringOrderingPagination;
using FilteringOrderingPagination.Models;
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
    public async Task<ActionResult<IPagedList<ListGroupsResponse>>> GetGroups(FopRequest<Group, GroupFilter> request)
    {
        var pagedGroups = await _unitOfWork
            .GetRepository<Group>()
            .GetPagedListAsync(
                s => ListGroupsResponse.FromGroup(s),
                request);

        return Ok(pagedGroups);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetGroupResponse>> GetGroup(long id)
    {
        var group = await _unitOfWork
            .GetRepository<Group>()
            .FindAsync(id);

        if (group == null) return NotFound();

        return GetGroupResponse.FromGroup(group);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGroup(CreateGroupRequest request)
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
    public async Task<ActionResult<GetGroupResponse>> UpdateGroup(UpdateGroupRequest request, long id)
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
}