using System;
using System.Linq;
using System.Threading.Tasks;
using FindYourFlix.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FindYourFlix.Data
{
    public class Repository : IRepository
    {
        private readonly ApplicationContext _context;

        public Repository(ApplicationContext context)
        {
            _context = context;
        }
        
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        
        
        public virtual IQueryable<TEntity> Query<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>();
        }
        
        public virtual void Insert<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Add(entity);
        }
        
        public virtual async Task InsertAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }
        
        public virtual void Delete<TEntity>(object id) where TEntity : class
        {
            var entityToDelete = _context.Set<TEntity>().Find(id);
            Delete(entityToDelete);
        }
        
        protected virtual void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Set<TEntity>().Attach(entity);
            }

            _context.Set<TEntity>().Remove(entity);
        }
        
        public virtual void Update<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<TEntity> GetByIdAsync<TEntity>(string id) where TEntity : class
        {
            if (id == String.Empty)
            {
                throw new ArgumentException($"{nameof(GetByIdAsync)} id must not be empty");
            }

            return await _context.FindAsync<TEntity>(id);
        }
    }
}