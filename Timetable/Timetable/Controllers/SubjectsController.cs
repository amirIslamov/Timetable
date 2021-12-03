using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Timetable.Timetable.Dto;

namespace Timetable.Timetable.Controllers
{
    [Route("api/v1/subjects")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        public async Task<ActionResult<ListSubjectsResponse>> GetSubjects()
        {
            throw new NotImplementedException();
        }
        
        public async Task<ActionResult<GetSubjectResponse>> GetSubject()
        {
            throw new NotImplementedException();
        }
        
        public async Task<IActionResult> CreateSubject(CreateSubjectRequest request)
        {
            throw new NotImplementedException();
        }
        
        public async Task<ActionResult<GetSubjectResponse>> UpdateSubject(UpdateSubjectRequest request)
        {
            throw new NotImplementedException();
        }
    }
}