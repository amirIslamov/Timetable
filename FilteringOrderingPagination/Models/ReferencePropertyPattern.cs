using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FilteringOrderingPagination.Models.Specifications;

namespace FilteringOrderingPagination.Models;

public class ReferencePropertyPattern<T>: Pattern<T>
    where T : class
{
    public T Eq { get; set; }
    public T Neq { get; set; }
    public IList<T> OneOf { get; set; }

    protected override IList<Expression<Func<T, bool>>> GetPredicateList()
        => new List<Expression<Func<T, bool>>>
            {
                Eq == null ? null : x => x.Equals(Eq),
                Neq == null ? null : x => !x.Equals(Neq),
                OneOf == null || OneOf.Count == 0 ? null : x => OneOf.Contains(x)
            }
            .Where(x => x != null)
            .ToList();
}