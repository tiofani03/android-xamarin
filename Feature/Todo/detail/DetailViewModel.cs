using System.Threading.Tasks;
using AndroidX.Lifecycle;
using productDemo.Data.Todo.api.repo;

namespace productDemo.Feature.Todo.detail
{
    public class DetailViewModel : ViewModel
    {
        private readonly ITodoRepository _todoRepository;

        public DetailViewModel(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }


        public async Task<Data.Todo.implementation.local.Todo> GetDataById(int id)
        {
            return await _todoRepository.GetDataById(id);
        }

        public void DeleteData(Data.Todo.implementation.local.Todo todo)
        {
            _todoRepository.DeleteData(todo);
        }
    }
}