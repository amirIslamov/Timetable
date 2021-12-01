using System;
using System.Linq;
using System.Linq.Expressions;

namespace Repositories.Abstractions.Repositories
{
    public interface IRepositoryQuery<TEntity> : IQueryable<TEntity>{
        IRepositoryQuery<TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> path);
        IRepositoryQuery<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
    }
}