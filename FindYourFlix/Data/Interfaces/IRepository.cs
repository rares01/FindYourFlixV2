using System;
using System.Linq;
using System.Threading.Tasks;

namespace FindYourFlix.Data.Interfaces
{
    public interface IRepository : IDisposable
    {
        IQueryable<TEntity> Query<TEntity>() where TEntity : class;
        void Insert<TEntity>(TEntity entity) where TEntity : class;
        Task InsertAsync<TEntity>(TEntity entity) where TEntity : class;
        void Delete<TEntity>(object id) where TEntity : class;
        void Update<TEntity>(TEntity entity) where TEntity : class;
        Task<TEntity> GetByIdAsync<TEntity> (string id) where TEntity : class;
        Task<int> SaveAsync();
    }
}