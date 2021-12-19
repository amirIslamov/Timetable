using System;
using System.Linq.Expressions;

namespace FilteringOrderingPagination.Models.Specifications;

public class Specification<T>
{
    public Specification(Expression<Func<T, bool>> expression)
    {
        Expression = expression;
    }

    public virtual Expression<Func<T, bool>> Expression { get; }

    public bool IsSatisfiedBy(T entity)
    {
        var predicate = Expression.Compile();
        return predicate(entity);
    }
}