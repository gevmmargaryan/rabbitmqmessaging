using Hash.DAL.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hash.Service.Interfaces
{
    public interface IEntityService<T> where T : class, IEntity
    {
        Task<IEnumerable<T>> AllAsync();
        Task<T> AddAsync(T entity);
    }
}
