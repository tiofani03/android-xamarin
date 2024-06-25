using System.Collections.Generic;
using System.Threading.Tasks;
using AndroidX.Lifecycle;
using productDemo.Data.Chucker.api.model;
using productDemo.Data.Chucker.api.repo;

namespace productDemo.Feature.Chucker.list
{
    public class ListChuckerViewModel : ViewModel
    {
        private readonly IChuckerRepository _chuckerRepository;
        
        public ListChuckerViewModel(IChuckerRepository chuckerRepository)
        {
            _chuckerRepository = chuckerRepository;
        }
        
        public async Task<List<Traffic>> LoadData()
        {
            return await _chuckerRepository.ReadTraffic();
        }
    }
}