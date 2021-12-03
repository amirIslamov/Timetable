using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repositories.Abstractions
{
    public abstract class RepositoryQueryBase<TEntity> : IQueryable<TEntity> {
        readonly Lazy<IQueryable<TEntity>> _queryable;
        protected IQueryable<TEntity> Queryable { get { return _queryable.Value; } }
        protected RepositoryQueryBase(Func<IQueryable<TEntity>> getQueryable) {
            this._queryable = new Lazy<IQueryable<TEntity>>(getQueryable);
        }
        Type IQueryable.ElementType { get { return this.Queryable.ElementType; } }
        Expression IQueryable.Expression { get { return this.Queryable.Expression; } }
        IQueryProvider IQueryable.Provider { get { return this.Queryable.Provider; } }
        
        IEnumerator IEnumerable.GetEnumerator() { return this.Queryable.GetEnumerator(); }
        public IEnumerator<TEntity> GetEnumerator() { return this.Queryable.GetEnumerator(); }
    }
}