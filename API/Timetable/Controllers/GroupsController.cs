using System.Linq;
using System.Threading.Tasks;
using API.Timetable.Dto;
using API.Timetable.Dto.Group;
using Microsoft.AspNetCore.Mvc;
using Model.Dal.Repositories;
using Repositories.Util;
using Timetable.Timetable.Model;

namespace API.Timetable.Controllers
{
    [Route("api/v1/groups")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        protected GroupsRepository _groupsRepository;
        protected TeachersRepository _teachersRepository;

        public GroupsController(GroupsRepository groupsRepository, TeachersRepository teachersRepository)
        {
            _groupsRepository = groupsRepository;
            _teachersRepository = teachersRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ListGroupsResponse>> GetGroups([FromQuery] ListGroupsRequest request)
        {
            var paging = new Paging(request.PageNum, request.PageSize);
            var groups = _groupsRepository.Paginate(paging).ToList();

            return ListGroupsResponse.FromGroupsAndPaging(groups, paging);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<GetGroupResponse>> GetGroup(long id)
        {
            var group = await _groupsRepository.FindAsync(id);

            if (group == null)
            {
                return NotFound();
            }

            return GetGroupResponse.FromGroup(group);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateGroup(CreateGroupRequest request)
        {
            var group = new Group()
            {
                Name = request.Name,
                ShortName = request.ShortName,
                AdmissionYear = request.AdmissionYear
            };
            
            var curator = await _teachersRepository.FindAsync(request.CuratorId);

            if (curator == null)
            {
                return BadRequest();
            }

            group.Curator = curator;
            
            _groupsRepository.Insert(group);
            await _groupsRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGroup), 
                new {id = group.Id},
                GetGroupResponse.FromGroup(group));
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<GetGroupResponse>> UpdateGroup(UpdateGroupRequest request, long id)
        {
            var group = await _groupsRepository.FindAsync(id);

            if (group == null)
            {
                return NotFound();
            }

            group.Name  = request.Name;
            group.ShortName = request.ShortName;
            group.AdmissionYear = request.AdmissionYear;

            if (request.CuratorId != group.CuratorId)
            {
                var curator = await _teachersRepository.FindAsync(request.CuratorId);

                if (curator == null)
                {
                    return BadRequest();
                }

                group.Curator = curator;
                group.CuratorId = curator.Id;
            }
            
            _groupsRepository.Update(group);
            await _groupsRepository.SaveChangesAsync();

            return GetGroupResponse.FromGroup(group);
        }
    }
}