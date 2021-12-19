using System;
using System.Linq.Expressions;

namespace FilteringOrderingPagination.Models.Specifications;

public static class SpecificationExtensions
{
    public static Specification<T> And<T>(
        this Specification<T> left,
        Specification<T> right)
        => new Specification<T>(
            left.Expression.And(right.Expression));

    public static Specification<T> Or<T>(
        this Specification<T> left,
        Specification<T> right)
        => new Specification<T>(
            left.Expression.Or(right.Expression));
    
    public static Specification<T> And<T>(
        this Specification<T> left,
        Expression<Func<T, bool>> right)
        => new Specification<T>(
            left.Expression.And(right));

    public static Specification<T> Or<T>(
        this Specification<T> left,
        Expression<Func<T, bool>> right)
        => new Specification<T>(
            left.Expression.Or(right));

    public static Specification<T> Not<T>(
        this Specification<T> argument)
        => new Specification<T>(argument.Expression.Not());
}