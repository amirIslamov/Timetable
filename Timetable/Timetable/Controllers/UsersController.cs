using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Dal.Identity;
using Model.Dal.Repositories;
using Repositories.Util;
using Timetable.Auth.Dto.Profile;
using Timetable.Timetable.Dto.User;

namespace Timetable.Timetable.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        protected UsersRepository _usersRepository;

        public UsersController(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ListUsersResponse>> GetUsers([FromQuery] GetUsersRequest request)
        {
            var paging = new Paging(request.PageNum, request.PageSize);
            var users = _usersRepository
                .Paginate(paging)
                .ToList();

            return ListUsersResponse.FromUsersAndPaging(users, paging);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<GetProfileResponse>> GetUser(long id)
        {
            var user = _usersRepository.Find(id);

            if (user != null)
            {
                return GetProfileResponse.FromUser(user);
            }

            return NotFound();
        }
        
        [HttpGet("self")]
        public async Task<ActionResult<GetProfileResponse>> GetSelf()
        {
            var userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            var user = _usersRepository.Find(userId);

            if (user != null)
            {
                return GetProfileResponse.FromUser(user);
            }

            return NotFound();
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<GetProfileResponse>> UpdateUser(UpdateProfileRequest request, long id)
        {
            var user = _usersRepository.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            user.Address = request.Address;
            user.PhoneNumber = request.PhoneNumber;
            user.FullName = request.FullName;
            
            _usersRepository.Update(user);
            await _usersRepository.SaveChangesAsync();

            return GetProfileResponse.FromUser(user);
        }
    }
}