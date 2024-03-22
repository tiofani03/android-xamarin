using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using productDemo.Data.Todo.api.repo;
using productDemo.Data.Todo.implementation.local;

namespace productDemo.Data.Todo.implementation.repository
{
    public class TodoRepository : ITodoRepository
    {
        private static TodoDatabase _database;

        // Create the database connection as a singleton.
        public static TodoDatabase Database
        {
            get
            {
                if (_database == null)
                    _database = new TodoDatabase(Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3"));

                return _database;
            }
        }


        public Task<int> SaveData(local.Todo todo)
        {
            return Database.SaveNoteAsync(todo);
        }

        public Task<List<local.Todo>> ReadData()
        {
            return Database.GetNotesAsync();
        }

        public Task<local.Todo> GetDataById(int id)
        {
            return Database.GetNoteAsync(id);
        }

        public Task<int> DeleteData(local.Todo todo)
        {
            return Database.DeleteNoteAsync(todo);
        }
    }
}