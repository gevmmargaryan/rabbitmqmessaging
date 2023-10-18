using AutoMapper;
using Hash.DAL.Repositories.Implementations;
using Hash.DAL.Repositories.Interfaces;
using Hash.Model.ViewModels;
using Hash.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash.Service.Implementations
{
    public class HashService : IHashService
    {
        private readonly IHashRepository _hashRepository;
        private readonly IMapper _mapper;

        public HashService(IEntityRepository<DAL.Entities.Hash> entityRepository,
            IHashRepository hashRepository,
            IMapper mapper)
        {
            _hashRepository = hashRepository;
            _mapper = mapper;
        }

        public async Task<HashInsertModel> InsertAsync(HashInsertModel hashInsertModel)
        {
            DAL.Entities.Hash hash = _mapper.Map<DAL.Entities.Hash>(hashInsertModel);

            hash = await _hashRepository.InsertAsync(hash);

            return _mapper.Map<HashInsertModel>(hash);
        }

        public async Task<List<HashGroupedViewModel>> GetAllGroupedAsync()
        {
            var groupedHashes = await _hashRepository.All()
                .GroupBy(h => h.Date)
                .Select(x => new HashGroupedViewModel { Date = DateOnly.FromDateTime(x.Key), Count = x.Count() })
                .ToListAsync();

            return groupedHashes;
        }
    }
}
