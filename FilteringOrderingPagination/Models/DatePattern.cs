using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FilteringOrderingPagination.Models.Specifications;

namespace FilteringOrderingPagination.Models;

public class DatePattern : ValuePropertyPattern<DateTime>
{
    public DateTime? Before { get; set; }
    public DateTime? After { get; set; }

    protected override IList<Expression<Func<DateTime, bool>>> GetPredicateList()
        => new List<Expression<Func<DateTime, bool>>>
            {
                Before == null ? null : x => x <= Before,
                After == null ? null : x => x >= After
            }
            .Where(x => x != null)
            .Concat(base.GetPredicateList())
            .ToList();
}