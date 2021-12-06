using System;
using System.Threading.Tasks;
using API.Auth.Dto.Auth;
using API.Auth.Dto.Profile;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.Profile;

namespace API.Auth.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class LoginController : ControllerBase
    {
        private UserManager<TimetableUser> _userManager;
        private JwtService _jwtService;
        private IUserClaimsPrincipalFactory<TimetableUser> _principalFactory;

        public LoginController(
            UserManager<TimetableUser> userManager,
            JwtService jwtService,
            IUserClaimsPrincipalFactory<TimetableUser> principalFactory)
            => (_userManager, _jwtService, _principalFactory) = (userManager, jwtService, principalFactory);

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (!await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return BadRequest();
            }

            var principal = await _principalFactory.CreateAsync(user);
            var token = _jwtService.CreateToken(principal);

            return new LoginResponse()
            {
                Token = token
            };
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _userManager.CreateAsync(
                new TimetableUser()
                {
                    Email = request.Email,
                    FullName = request.FullName,
                    Address = request.Address,
                    PhoneNumber = request.PhoneNumber
                },
                request.Password
            );

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
        
        [HttpPost("forgot-password")]
        public async Task<IActionResult> RequestPasswordReset(RequestPasswordResetRequest request)
        {
            throw new NotImplementedException();
        }
        
        [HttpPost("password-reset")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            throw new NotImplementedException();
        }
    }
}