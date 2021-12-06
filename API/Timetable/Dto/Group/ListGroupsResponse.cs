using System.Collections.Generic;
using System.Linq;
using Repositories.Util;

namespace API.Timetable.Dto
{
    public class ListGroupsResponse
    {
        public List<ShortGroup> Groups { get; set; }
        public Paging Paging { get; set; }

        public class ShortGroup
        {
            public long Id { get; set; }
            public string ShortName { get; set; }
            public static ShortGroup FromGroup(global::Timetable.Timetable.Model.Group group) => new ShortGroup()
            {
                Id = group.Id,
                ShortName = group.ShortName
            };
        }

        public static ListGroupsResponse FromGroupsAndPaging(IEnumerable<global::Timetable.Timetable.Model.Group> groups, Paging paging)
            => new ListGroupsResponse()
            {
                Paging = paging,
                Groups = groups.Select(ShortGroup.FromGroup).ToList()
            };
    }
}