using System;
using System.Threading.Tasks;
using API.Timetable.Dto;
using Microsoft.AspNetCore.Mvc;

namespace API.Timetable.Controllers
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