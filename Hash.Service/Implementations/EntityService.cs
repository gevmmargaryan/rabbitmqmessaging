using Hash.DAL.Entities.Interfaces;
using Hash.DAL.Repositories.Interfaces;
using Hash.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash.Service.Implementations
{
    public class EntityService<T> : IEntityService<T> where T : class, IEntity
    {
        private readonly IEntityRepository<T> _entityRepository;
        public EntityService(IEntityRepository<T> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public async Task<IEnumerable<T>> AllAsync()
        {
            return await _entityRepository.All().ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            return await _entityRepository.InsertAsync(entity);
        }
    }
}
