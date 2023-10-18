using Hash.DAL.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash.DAL.Repositories.Interfaces
{
    public interface IEntityRepository<T> where T : class, IEntity
    {
        IQueryable<T> All();
        Task<T> InsertAsync(T entity);

    }
}
