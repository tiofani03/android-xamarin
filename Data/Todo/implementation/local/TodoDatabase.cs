using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace productDemo.Data.Todo.implementation.local
{
    public class TodoDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public TodoDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Todo>().Wait();
        }

        public Task<List<Todo>> GetNotesAsync()
        {
            //Get all notes.
            return _database.Table<Todo>().ToListAsync();
        }

        public Task<Todo> GetNoteAsync(int id)
        {
            // Get a specific note.
            return _database.Table<Todo>()
                .Where(i => i.ID == id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> SaveNoteAsync(Todo note)
        {
            // return _database.UpdateAsync(note);
            var existing = await GetNoteAsync(note.ID);
            if (existing != null)
            {
                return await _database.UpdateAsync(note);
            }
            else
            {
                return await _database.InsertAsync(note);
            }
        }
        
        public Task<int> InsertNoteAsync(Todo note)
        {
            return _database.InsertAsync(note);
        }
        
        

        public Task<int> DeleteNoteAsync(Todo note)
        {
            return _database.DeleteAsync(note);
        }
    }
}