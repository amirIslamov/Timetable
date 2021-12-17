using System.Linq.Expressions;

namespace FilteringOrderingPagination.Models.Expressions;

public static class ExpressionsExtensions
{
    public static Expression<Func<TIn, TOut>> Chain<TIn, TInterstitial, TOut>(
        this Expression<Func<TIn, TInterstitial>> inner,
        Expression<Func<TInterstitial, TOut>> outer)
    {
        var visitor = new SwapVisitor(outer.Parameters[0], inner.Body);
        return Expression.Lambda<Func<TIn, TOut>>(visitor.Visit(outer.Body), inner.Parameters);
    }
}