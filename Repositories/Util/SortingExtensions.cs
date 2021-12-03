using System;
using System.Linq;

namespace Repositories.Util
{
    public static class SortingExtensions
    {
        public static IQueryable<T> Sort<T>(this IQueryable<T> queryable, Sort<T> sort)
        {
            var result = queryable;
            foreach (var stage in sort.Stages)
            {
                result = stage.Direction switch
                {
                    Util.Sort<T>.Direction.Ascending => result.OrderBy(stage.PropertyExpression),
                    Util.Sort<T>.Direction.Descending => result.OrderByDescending(stage.PropertyExpression),
                    _ => throw new ArgumentOutOfRangeException(nameof(sort))
                };
            }

            return result;
        }
    }
}