using System;
using System.Linq;

namespace TicketHubApp.Interfaces
{
    public interface IRepository<TEntity> : IDisposable
    {
        void Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        IQueryable<TEntity> GetAll();

        void SaveChanges();
    }
}
