using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.Abstractions
{
     public interface IRepository<TEntity, TKey>
    {
        /*Querying methods*/
        TEntity Find(TKey key);
        IEnumerable<TEntity> GetAll();

        /*Creating methods*/
        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);

        /*Updating methods*/
        void Update(TEntity entity);
        void Upsert(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void UpsertRange(IEnumerable<TEntity> entities);

        /*Deleting methods*/
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entity);

        /*Optional async overloads without cancellation token*/

        /*Querying methods*/
        Task<TEntity> FindAsync(TKey key) => throw new NotImplementedException();
        Task<IEnumerable<TEntity>> GetAllAsync() => throw new NotImplementedException();

        /*Creating methods*/
        Task InsertAsync(TEntity entity) => throw new NotImplementedException();
        Task InsertRangeAsync(IEnumerable<TEntity> entities) => throw new NotImplementedException();


        /*Updating methods*/
        Task UpdateAsync(TEntity entity) => throw new NotImplementedException();
        Task UpsertAsync(TEntity entity) => throw new NotImplementedException();
        Task UpdateRangeAsync(IEnumerable<TEntity> entities) => throw new NotImplementedException();
        Task UpsertRangeAsync(IEnumerable<TEntity> entities) => throw new NotImplementedException();

        /*Deleting methods*/
        Task RemoveAsync(TEntity entity) => throw new NotImplementedException();
        Task RemoveRangeAsync(IEnumerable<TEntity> entity) => throw new NotImplementedException();

        /*Optional async overloads with cancellation token*/

        /*Querying methods*/
        Task<TEntity> FindAsync(TKey key, CancellationToken token) => throw new NotImplementedException();
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken token) => throw new NotImplementedException();

        /*Creating methods*/
        Task InsertAsync(TEntity entity, CancellationToken token) => throw new NotImplementedException();
        Task InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken token) => throw new NotImplementedException();


        /*Updating methods*/
        Task UpdateAsync(TEntity entity, CancellationToken token) => throw new NotImplementedException();
        Task UpsertAsync(TEntity entity, CancellationToken token) => throw new NotImplementedException();
        Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken token) => throw new NotImplementedException();
        Task UpsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken token) => throw new NotImplementedException();

        /*Deleting methods*/
        Task RemoveAsync(TEntity entity, CancellationToken token) => throw new NotImplementedException();
        Task RemoveRangeAsync(IEnumerable<TEntity> entity, CancellationToken token) => throw new NotImplementedException();
    }
}