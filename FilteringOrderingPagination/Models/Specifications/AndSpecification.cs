using Expr = System.Linq.Expressions;

namespace FilteringOrderingPagination.Models.Specifications;

public class AndSpecification<T> : Specification<T>
{
    public AndSpecification(params Specification<T>[] specifications)
        : base(GetAndExpression(specifications.Select(s => s.Expression).ToArray()))
    {
    }

    protected static Expr.Expression<Func<T, bool>> GetAndExpression(Expr.Expression<Func<T, bool>>[] expressions)
    {
        if (expressions.Length == 1) return expressions[0];

        var result = Expr.Expression.AndAlso(
            expressions[0].Body,
            expressions[1].Body);

        for (var i = 2; i < expressions.Length; i++)
            result = Expr.Expression.AndAlso(
                result, expressions[i].Body);

        return Expr.Expression.Lambda<Func<T, bool>>(
            result, expressions[0].Parameters.Single());
    }
}