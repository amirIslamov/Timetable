namespace FilteringOrderingPagination.Models.Sort;

public static class QueryableExtensions
{
    public static IOrderedQueryable<T> Sort<T>(this IQueryable<T> queryable, Sort<T> sort)
    {
        var result = queryable;
        foreach (var stage in sort.Stages)
            result = stage.Direction switch
            {
                FilteringOrderingPagination.Models.Sort.Sort<T>.Direction.Ascending => result.OrderBy(
                    stage.PropertyExpression),
                FilteringOrderingPagination.Models.Sort.Sort<T>.Direction.Descending => result.OrderByDescending(
                    stage.PropertyExpression),
                _ => throw new ArgumentOutOfRangeException(nameof(sort))
            };

        return (IOrderedQueryable<T>) result;
    }
}