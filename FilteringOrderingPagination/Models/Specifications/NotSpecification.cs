using Expr = System.Linq.Expressions;

namespace FilteringOrderingPagination.Models.Specifications;

public class NotSpecification<T> : Specification<T>
{
    public NotSpecification(Specification<T> specification) : base(GetNotExpression(specification.Expression))
    {
    }

    protected static Expr.Expression<Func<T, bool>> GetNotExpression(Expr.Expression<Func<T, bool>> expression)
    {
        var notExpr = Expr.Expression.Not(expression.Body);

        return Expr.Expression.Lambda<Func<T, bool>>(
            notExpr, expression.Parameters.Single());
    }
}