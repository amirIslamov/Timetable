using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FilteringOrderingPagination.Models.Specifications;

namespace FilteringOrderingPagination.Models;

public abstract class Pattern<T>
{
    protected abstract IList<Expression<Func<T, bool>>> GetPredicateList();

    public Specification<TEntity> AppliedTo<TEntity>(Expression<Func<TEntity, T>> selector)
    {
        var exprList = GetPredicateList();
        if (exprList == null || exprList.Count == 0)
            return new Specification<TEntity>(x => true);

        var resultExpression = exprList
            .Select(e => selector.Chain(e))
            .Aggregate((l, r) => l.And(r));

        return new Specification<TEntity>(resultExpression);
    }
}