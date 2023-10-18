using Hash.Model.ViewModels;
using AutoMapper;

namespace Hash.Api.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DAL.Entities.Hash, HashInsertModel>();

            CreateMap<HashInsertModel, DAL.Entities.Hash>();
        }
    }
}
