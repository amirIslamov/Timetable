using API.Timetable.Dto.User;
using Arch.EntityFrameworkCore.UnitOfWork;
using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using FilteringOrderingPagination;
using FilteringOrderingPagination.Models;
using Microsoft.AspNetCore.Mvc;
using Model.Auth;
using Model.Dal;
using Model.Entities;

namespace API.Timetable.Controllers;

[Route("api/v1/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUnitOfWork<TimetableDbContext> _unitOfWork;
    private readonly TimetableUserManager _userManager;

    public UsersController(TimetableUserManager userManager, IUnitOfWork<TimetableDbContext> unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<IPagedList<ListUsersResponse>>> GetUsers(
        FopRequest<TimetableUser, UserFilter> request)
    {
        var pagedUsers = await _unitOfWork
            .GetRepository<TimetableUser>()
            .GetPagedListAsync(
                u => ListUsersResponse.FromUser(u),
                request);

        return Ok(pagedUsers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetProfileResponse>> GetUser(long id)
    {
        var user = await _userManager.FindAsync(id);

        if (user != null) return GetProfileResponse.FromUser(user);

        return NotFound();
    }

    [HttpGet("self")]
    public async Task<ActionResult<GetProfileResponse>> GetSelf()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User);

        if (user != null) return GetProfileResponse.FromUser(user);

        return NotFound();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GetProfileResponse>> UpdateUser(UpdateProfileRequest request, long id)
    {
        var user = await _userManager.FindAsync(id);

        if (user == null) return NotFound();

        user.Address = request.Address;
        user.PhoneNumber = request.PhoneNumber;
        user.FullName = request.FullName;

        await _userManager.UpdateAsync(user);

        await _unitOfWork.SaveChangesAsync();

        return GetProfileResponse.FromUser(user);
    }
}