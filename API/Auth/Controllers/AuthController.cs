using API.Auth.Authorization;
using API.Auth.Dto.Auth;
using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Model.Auth;
using Model.Dal;
using Model.Entities;
using Model.Users;

namespace API.Auth.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class LoginController : ControllerBase
{
    private readonly JwtService _jwtService;
    private readonly PasswordManager _passwordManager;
    private readonly IUserClaimsPrincipalFactory _principalFactory;
    private readonly TimetableUserManager _userManager;
    private readonly IUnitOfWork<TimetableDbContext> _unitOfWork;

    public LoginController(JwtService jwtService, TimetableUserManager userManager, PasswordManager passwordManager,
        IUserClaimsPrincipalFactory principalFactory, IUnitOfWork<TimetableDbContext> unitOfWork)
    {
        _jwtService = jwtService;
        _userManager = userManager;
        _passwordManager = passwordManager;
        _principalFactory = principalFactory;
        _unitOfWork = unitOfWork;
    }


    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            return BadRequest();
        }

        if (await _passwordManager.CheckPasswordAsync(user, request.Password) == PasswordVerificationResult.Failed)
            return BadRequest();

        var principal = await _principalFactory.CreateAsync(user);
        var token = _jwtService.CreateToken(principal);

        return new LoginResponse
        {
            Token = token
        };
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await _userManager.CreateAsync(
            new TimetableUser
            {
                Email = request.Email,
                FullName = request.FullName,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber
            },
            request.Password
        );

        if (!result.Succeeded) return BadRequest(result.Failure);

        await _unitOfWork.SaveChangesAsync();

        return Ok();
    }
}