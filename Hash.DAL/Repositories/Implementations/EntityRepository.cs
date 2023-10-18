using Hash.DAL.Context;
using Hash.DAL.Entities.Interfaces;
using Hash.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash.DAL.Repositories.Implementations
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class, IEntity
    {
        private readonly ApplicationDbContext _context;
        protected readonly DbSet<T> Entities;
        string _errorMessage = string.Empty;

        public EntityRepository(ApplicationDbContext context)
        {
            _context = context;
            Entities = context.Set<T>();
        }
        public IQueryable<T> All()
        {
            return Entities;
        }
        public async Task<T> InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            Entities.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
