using System;
using System.Linq;
using System.Linq.Expressions;

namespace FilteringOrderingPagination.Models.Specifications;

public static class ExpressionsExtensions
{
    public static Expression<Func<TIn, TOut>> Chain<TIn, TInterstitial, TOut>(
        this Expression<Func<TIn, TInterstitial>> inner,
        Expression<Func<TInterstitial, TOut>> outer)
    {
        var visitor = new SwapVisitor(outer.Parameters[0], inner.Body);
        return Expression.Lambda<Func<TIn, TOut>>(visitor.Visit(outer.Body), inner.Parameters);
    }

    public static Expression<Func<TIn, bool>> And<TIn>(
        this Expression<Func<TIn, bool>> left,
        Expression<Func<TIn, bool>> right)
    {
        var substitutedRight = right.SubstituteParameter(left);
        
        var andExpression = Expression.AndAlso(
            left.Body,
            substitutedRight.Body);

        return Expression.Lambda<Func<TIn, bool>>(andExpression, left.Parameters.Single());
    }
    
    public static Expression<Func<TIn, bool>> Or<TIn>(
        this Expression<Func<TIn, bool>> left,
        Expression<Func<TIn, bool>> right)
    {
        var substitutedRight = right.SubstituteParameter(left);
        
        var orExpression = Expression.OrElse(
            left.Body,
            substitutedRight.Body);

        return Expression.Lambda<Func<TIn, bool>>(orExpression, left.Parameters.Single());
    }
    
    public static Expression<Func<TIn, bool>> Not<TIn>(
        this Expression<Func<TIn, bool>> argument)
    {
        var notExpression = Expression.Not(argument.Body);

        return Expression.Lambda<Func<TIn, bool>>(notExpression, argument.Parameters.Single());
    }

    private static Expression<Func<TIn, TOut>> SubstituteParameter<TIn, TOut>(
        this Expression<Func<TIn, TOut>> toSubstitute,
        Expression<Func<TIn, TOut>> substituteWith)
    {
        var visitor = new SwapVisitor(
            toSubstitute.Parameters.Single(), 
            substituteWith.Parameters.Single());
        return Expression.Lambda<Func<TIn, TOut>>(visitor.Visit(toSubstitute.Body), substituteWith.Parameters);
    }
}