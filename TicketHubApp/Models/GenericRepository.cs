using System;
using System.Data.Entity;
using System.Linq;
using TicketHubApp.Interfaces;

namespace TicketHubApp.Models
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbContext _context;
        public DbContext Context { get { return _context; } }
        public GenericRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentException();
        }
        public void Create(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            _context.Entry(entity).State = EntityState.Added;
            SaveChanges();
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            _context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            _context.Entry(entity).State = EntityState.Deleted;
            SaveChanges();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && _context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }

    }
}
