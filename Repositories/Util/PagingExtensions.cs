using System;
using System.Collections.Generic;
using System.Linq;

namespace Repositories.Util
{
    public static class PagingExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, Paging paging)
            => paging switch
            {
                {IsUnpaged: true} => queryable,
                { } => queryable.Skip<T>((int) paging.Offset).Take<T>(paging.PageSize),
                null => throw new ArgumentNullException(nameof(paging))
            };
    }
}