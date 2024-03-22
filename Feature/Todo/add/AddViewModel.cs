using System;
using System.Threading.Tasks;
using AndroidX.Lifecycle;
using productDemo.Data.Todo.api.repo;

namespace productDemo.Feature.Todo.add
{
    public class AddViewModel : ViewModel
    {
        private readonly ITodoRepository _todoRepository;

        public AddViewModel(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }
        
        public async Task<Data.Todo.implementation.local.Todo> GetDataById(int id)
        {
            return await _todoRepository.GetDataById(id);
        }
        
        public async Task InsertData(Data.Todo.implementation.local.Todo todo)
        {
            await _todoRepository.SaveData(todo);
        }
    }
}