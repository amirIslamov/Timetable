using System.Linq;

namespace Repositories.Util
{
    public static class SpecificationExtensions
    {
        public static IQueryable<T> Specify<T>(this IQueryable<T> queryable, ExpressionSpecification<T> specification)
        {
            return queryable.Where(specification.Expression);
        }
    }
}