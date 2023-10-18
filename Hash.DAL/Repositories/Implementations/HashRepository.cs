using Hash.DAL.Context;
using Hash.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash.DAL.Repositories.Implementations
{
    public class HashRepository : EntityRepository<Entities.Hash>, IHashRepository
    {
        public HashRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
