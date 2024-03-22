using System.Collections.Generic;
using System.Threading.Tasks;

namespace productDemo.Data.Todo.api.repo
{
    public interface ITodoRepository
    {
        public Task<int> SaveData(implementation.local.Todo todo);
        public Task<List<implementation.local.Todo>> ReadData();
        public Task<implementation.local.Todo> GetDataById(int id);
        public Task<int> DeleteData(implementation.local.Todo todo);
    }
}