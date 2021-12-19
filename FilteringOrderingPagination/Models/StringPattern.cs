using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FilteringOrderingPagination.Models;

public class StringPattern : ReferencePropertyPattern<string>
{
    public string Contains { get; set; }
    public string StartsWith { get; set; }
    public string EndsWith { get; set; }

    protected override IList<Expression<Func<string, bool>>> GetPredicateList()
        => new List<Expression<Func<string, bool>>>
            {
                string.IsNullOrEmpty(Contains) ? null : x => x.Contains(Contains),
                string.IsNullOrEmpty(StartsWith) ? null : x => x.StartsWith(StartsWith),
                string.IsNullOrEmpty(EndsWith) ? null : x => x.EndsWith(EndsWith),
            }
            .Where(x => x != null)
            .Concat(base.GetPredicateList())
            .ToList();
}