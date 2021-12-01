using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Repositories.EntityFramework;

namespace Repositories.EnitityFramework
{
    public abstract class DbRepository<TEntity, TKey, TDbContext>: DbRepositoryQuery<TEntity>, IRepository<TEntity, TKey>, IDisposable, IAsyncDisposable
        where TEntity: class
        where TDbContext: DbContext
    {
        protected readonly TDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public DbRepository(TDbContext context)
            : base(context.Set<TEntity>)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }
        
        public virtual TEntity Find(TKey key)
        {
            return DbSet.Find(key);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return this.Where(_ => true).ToList();
        }

        public virtual async Task<TEntity> FindAsync(TKey key)
        {
            return await this.DbSet.FindAsync(key);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await this.Where(_ => true).ToListAsync();
        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public virtual void Upsert(TEntity entity)
        {
            Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            DbSet.UpdateRange(entities);
        }

        public virtual void UpsertRange(IEnumerable<TEntity> entities)
        {
            UpdateRange(entities);
        }

        public virtual void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
        
        public void Dispose()
        {
            Context.Dispose();
        }

        public ValueTask DisposeAsync()
        {
            return Context.DisposeAsync();
        }
    }
}