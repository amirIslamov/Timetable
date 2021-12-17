using Expr = System.Linq.Expressions;

namespace FilteringOrderingPagination.Models.Specifications;

public class OrSpecification<T> : Specification<T>
{
    public OrSpecification(params Specification<T>[] specifications)
        : base(GetOrExpression(specifications.Select(s => s.Expression).ToArray()))
    {
    }

    protected static Expr.Expression<Func<T, bool>> GetOrExpression(Expr.Expression<Func<T, bool>>[] expressions)
    {
        if (expressions.Length == 1) return expressions[0];

        var result = Expr.Expression.OrElse(
            expressions[0].Body,
            expressions[1].Body);

        for (var i = 2; i < expressions.Length; i++)
            result = Expr.Expression.AndAlso(
                result, expressions[i].Body);

        return Expr.Expression.Lambda<Func<T, bool>>(
            result, expressions[0].Parameters.Single());
    }
}