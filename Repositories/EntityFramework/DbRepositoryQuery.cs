using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstractions;

namespace Repositories.EntityFramework
{
    public class DbRepositoryQuery<TEntity> : RepositoryQueryBase<TEntity>, IRepositoryQuery<TEntity>
        where TEntity : class
    {
        public DbRepositoryQuery(Func<IQueryable<TEntity>> getQueryable)
            : base(getQueryable)
        {
        }

        IRepositoryQuery<TEntity> IRepositoryQuery<TEntity>.Include<TProperty>(
            Expression<Func<TEntity, TProperty>> path)
        {
            return new DbRepositoryQuery<TEntity>(() => Queryable.Include(path));
        }

        IRepositoryQuery<TEntity> IRepositoryQuery<TEntity>.Where(Expression<Func<TEntity, bool>> predicate)
        {
            return new DbRepositoryQuery<TEntity>(() => Queryable.Where(predicate));
        }
    }
}