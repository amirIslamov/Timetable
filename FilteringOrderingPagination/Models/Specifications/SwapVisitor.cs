using System.Linq.Expressions;

namespace FilteringOrderingPagination.Models.Specifications;

public class SwapVisitor : ExpressionVisitor
{
    private readonly Expression _source, _replacement;

    public SwapVisitor(Expression source, Expression replacement)
    {
        _source = source;
        _replacement = replacement;
    }

    public override Expression Visit(Expression node)
    {
        return node == _source ? _replacement : base.Visit(node);
    }
}