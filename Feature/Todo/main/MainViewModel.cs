using System.Collections.Generic;
using System.Threading.Tasks;
using AndroidX.Lifecycle;
using productDemo.Data.Todo.api.repo;

namespace productDemo.Feature.Todo.main
{
    public class MainViewModel : ViewModel
    {
        private readonly ITodoRepository _todoRepository;


        public MainViewModel(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }


        public async Task<List<Data.Todo.implementation.local.Todo>> LoadData()
        {
            return await _todoRepository.ReadData();
        }
    }
}