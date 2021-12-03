using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Dal.Repositories;
using Model.Profile.Roles;
using Model.Timetable;
using Repositories.Util;
using Timetable.Timetable.Dto;
using Timetable.Timetable.Dto.Teacher;
using Timetable.Timetable.Model;

namespace Timetable.Timetable.Controllers
{
    [Route("api/v1/teachers")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        protected UsersRepository _usersRepository;
        protected TeachersRepository _teachersRepository;
        
        public TeachersController(UsersRepository usersRepository, TeachersRepository teachersRepository)
        {
            _usersRepository = usersRepository;
            _teachersRepository = teachersRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ListTeachersResponse>> GetTeachers([FromQuery] ListTeachersRequest request)
        {
            var paging = new Paging(request.PageNum, request.PageSize);
            var students = _teachersRepository
                .Include(t => t.User)
                .Paginate(paging);

            return ListTeachersResponse.FromTeachersAndPaging(students, paging);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<GetTeacherResponse>> GetTeacher(long id)
        {
            var teacher = await _teachersRepository
                .Include(t => t.User)
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();

            if (teacher == null)
            {
                return NotFound();
            }

            return GetTeacherResponse.FromTeacher(teacher);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateTeacher(CreateTeacherRequest request)
        {
            var user = _usersRepository.Find(request.UserId);
            if (user == null)
            {
                return BadRequest();
            }

            if (user.RoleSet.ContainsRole(Role.Teacher))
            {
                return BadRequest();
            }
            
            var teacher = new Teacher()
            {
                Id = user.Id,
                User = user,
                Chair = request.Chair
            };
            user.RoleSet.AddRole(Role.Student);
            
            _teachersRepository.Insert(teacher);
            await _teachersRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTeacher), 
                new {id = teacher.Id},
                GetTeacherResponse.FromTeacher(teacher));
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<GetTeacherResponse>> UpdateTeacher(UpdateTeacherRequest request, long id)
        {
            var teacher = await _teachersRepository
                .Include(t => t.User)
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();

            if (teacher == null)
            {
                return NotFound();
            }

            teacher.Chair = request.Chair;
            
            _teachersRepository.Update(teacher);
            await _teachersRepository.SaveChangesAsync();

            return GetTeacherResponse.FromTeacher(teacher);
        }
    }
}