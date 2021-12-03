using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Dal.Repositories;
using Model.Profile.Roles;
using Model.Timetable;
using Repositories.Util;
using Timetable.Timetable.Dto;
using Timetable.Timetable.Dto.Student;

namespace Timetable.Timetable.Controllers
{
    [Route("api/v1/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        protected StudentsRepository _studentsRepository;
        protected GroupsRepository _groupsRepository;
        protected UsersRepository _usersRepository;

        public StudentsController(StudentsRepository studentsRepository, GroupsRepository groupsRepository, UsersRepository usersRepository)
        {
            _studentsRepository = studentsRepository;
            _groupsRepository = groupsRepository;
            _usersRepository = usersRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ListStudentsResponse>> GetStudents([FromQuery] ListStudentsRequest request)
        {
            var paging = new Paging(request.PageNum, request.PageSize);
            var students = _studentsRepository
                .Include(s => s.User)
                .Paginate(paging);

            return ListStudentsResponse.FromStudentsAndPaging(students, paging);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<GetStudentResponse>> GetStudent(long id)
        {
            var student = await _studentsRepository
                .Include(s => s.User)
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();;

            if (student == null)
            {
                return NotFound();
            }

            return GetStudentResponse.FromStudent(student);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudentRequest request)
        {
            var user = _usersRepository.Find(request.UserId);
            if (user == null)
            {
                return BadRequest();
            }

            if (user.RoleSet.ContainsRole(Role.Student))
            {
                return BadRequest();
            }

            var group = _groupsRepository.Find(request.GroupId);

            if (group == null)
            {
                return BadRequest();
            }

            var student = new Student()
            {
                Id = user.Id,
                Group = group,
                User = user,
                FatherContacts = request.FatherContacts,
                MotherContacts = request.MotherContacts,
            };
            user.RoleSet.AddRole(Role.Student);
            
            _studentsRepository.Insert(student);
            await _studentsRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), 
                new {id = student.Id},
                GetStudentResponse.FromStudent(student));
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<GetStudentResponse>> UpdateStudent(UpdateStudentRequest request, long id)
        {
            var student = await _studentsRepository.Include(t => t.User)
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();;

            if (student == null)
            {
                return NotFound();
            }

            student.FatherContacts = request.FatherContacts;
            student.MotherContacts = request.MotherContacts;

            if (request.GroupId != student.GroupId)
            {
                var group = _groupsRepository.Find(request.GroupId);

                if (group == null)
                {
                    return BadRequest();
                }

                student.Group = group;
                student.GroupId = group.Id;
            }
            
            _studentsRepository.Update(student);
            await _studentsRepository.SaveChangesAsync();

            return GetStudentResponse.FromStudent(student);
        }
    }
}