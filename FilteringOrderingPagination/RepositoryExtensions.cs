using System.Linq.Expressions;
using Arch.EntityFrameworkCore.UnitOfWork;
using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using FilteringOrderingPagination.Models;
using FilteringOrderingPagination.Models.Paging;
using FilteringOrderingPagination.Models.Sort;
using FilteringOrderingPagination.Models.Specifications;
using Microsoft.EntityFrameworkCore.Query;

namespace FilteringOrderingPagination;

public static class RepositoryExtensions
{
    public static IPagedList<TEntity> GetPagedList<TEntity, TFilter>(
        this IRepository<TEntity> repository,
        FopRequest<TEntity, TFilter> request,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = true,
        CancellationToken cancellationToken = default,
        bool ignoreQueryFilters = false) where TEntity : class where TFilter : IFilter<TEntity>
    {
        return repository.GetPagedList(
            request?.Filter.ToSpecification().Expression,
            include: include,
            pageIndex: request.Paging.PageNumber,
            pageSize: request.Paging.PageSize,
            disableTracking: disableTracking,
            ignoreQueryFilters: ignoreQueryFilters
        );
    }

    public static Task<IPagedList<TEntity>> GetPagedListAsync<TEntity, TFilter>(
        this IRepository<TEntity> repository,
        FopRequest<TEntity, TFilter> request,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = true,
        CancellationToken cancellationToken = default,
        bool ignoreQueryFilters = false) where TEntity : class where TFilter : IFilter<TEntity>
    {
        return repository.GetPagedListAsync(
            request.Filter.ToSpecification().Expression,
            include: include,
            pageIndex: request.Paging.PageNumber,
            pageSize: request.Paging.PageSize,
            disableTracking: disableTracking,
            ignoreQueryFilters: ignoreQueryFilters,
            cancellationToken: cancellationToken
        );
    }

    public static IPagedList<TResult> GetPagedList<TEntity, TResult, TFilter>(
        this IRepository<TEntity> repository,
        Expression<Func<TEntity, TResult>> selector,
        FopRequest<TEntity, TFilter> request,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = true,
        bool ignoreQueryFilters = false) where TResult : class where TEntity : class where TFilter : IFilter<TEntity>
    {
        return repository.GetPagedList(
            selector,
            request.Filter.ToSpecification().Expression,
            include: include,
            pageIndex: request.Paging.PageNumber,
            pageSize: request.Paging.PageSize,
            disableTracking: disableTracking,
            ignoreQueryFilters: ignoreQueryFilters
        );
    }

    public static Task<IPagedList<TResult>> GetPagedListAsync<TEntity, TResult, TFilter>(
        this IRepository<TEntity> repository,
        Expression<Func<TEntity, TResult>> selector,
        FopRequest<TEntity, TFilter> request,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = true,
        CancellationToken cancellationToken = default,
        bool ignoreQueryFilters = false) where TResult : class where TEntity : class where TFilter : IFilter<TEntity>
    {
        return repository.GetPagedListAsync(
            selector,
            request.Filter.ToSpecification().Expression,
            include: include,
            pageIndex: request.Paging.PageNumber,
            pageSize: request.Paging.PageSize,
            disableTracking: disableTracking,
            ignoreQueryFilters: ignoreQueryFilters,
            cancellationToken: cancellationToken
        );
    }

    public static IPagedList<TEntity> GetPagedList<TEntity>(
        this IRepository<TEntity> repository,
        Specification<TEntity> specification,
        Paging paging,
        Sort<TEntity> sort,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = true,
        CancellationToken cancellationToken = default,
        bool ignoreQueryFilters = false) where TEntity : class
    {
        return repository.GetPagedList(
            predicate: specification.Expression,
            orderBy: q => q.Sort(sort),
            include,
            paging.PageNumber,
            paging.PageSize,
            disableTracking,
            ignoreQueryFilters
        );
    }

    public static Task<IPagedList<TEntity>> GetPagedListAsync<TEntity>(
        this IRepository<TEntity> repository,
        Specification<TEntity> specification,
        Paging paging,
        Sort<TEntity> sort,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = true,
        CancellationToken cancellationToken = default,
        bool ignoreQueryFilters = false) where TEntity : class
    {
        return repository.GetPagedListAsync(
            predicate: specification.Expression,
            orderBy: q => q.Sort(sort),
            include,
            paging.PageNumber,
            paging.PageSize,
            disableTracking,
            ignoreQueryFilters: ignoreQueryFilters,
            cancellationToken: cancellationToken
        );
    }

    public static IPagedList<TResult> GetPagedList<TEntity, TResult>(
        this IRepository<TEntity> repository,
        Expression<Func<TEntity, TResult>> selector,
        Specification<TEntity> specification,
        Paging paging,
        Sort<TEntity> sort,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = true,
        bool ignoreQueryFilters = false) where TResult : class where TEntity : class
    {
        return repository.GetPagedList(
            selector,
            specification.Expression,
            q => q.Sort(sort),
            include,
            paging.PageNumber,
            paging.PageSize,
            disableTracking,
            ignoreQueryFilters
        );
    }

    public static Task<IPagedList<TResult>> GetPagedListAsync<TEntity, TResult>(
        this IRepository<TEntity> repository,
        Expression<Func<TEntity, TResult>> selector,
        Specification<TEntity> specification,
        Paging paging,
        Sort<TEntity> sort,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = true,
        CancellationToken cancellationToken = default,
        bool ignoreQueryFilters = false) where TResult : class where TEntity : class
    {
        return repository.GetPagedListAsync(
            selector,
            specification.Expression,
            q => q.Sort(sort),
            include,
            paging.PageNumber,
            paging.PageSize,
            disableTracking,
            ignoreQueryFilters: ignoreQueryFilters,
            cancellationToken: cancellationToken
        );
    }
}