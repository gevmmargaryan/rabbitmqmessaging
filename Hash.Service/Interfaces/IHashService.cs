using Hash.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash.Service.Interfaces
{
    public interface IHashService
    {
        //This should be paginated, but will not implement here 
        Task<List<HashGroupedViewModel>> GetAllGroupedAsync();
        Task<HashInsertModel> InsertAsync(HashInsertModel hash);
    }
}
