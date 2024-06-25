using System.Collections.Generic;
using System.Threading.Tasks;
using productDemo.Data.Chucker.api.model;
using SQLite;

namespace productDemo.Data.Chucker.implementation
{
    public class ChuckerDatabase
    {
        private readonly SQLiteAsyncConnection _database;
        
        public ChuckerDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<api.model.Traffic>().Wait();
        }
        
        public Task<List<Traffic>> GetTrafficAsync()
        {
            return _database.Table<Traffic>()
                .OrderByDescending(t => t.RequestDate)
                .ToListAsync();
        }
        
        public Task<Traffic> GetTrafficAsync(string id)
        {
            return _database.Table<Traffic>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }
        
        public async Task<int> SaveTrafficAsync(Traffic traffic)
        {
            var existing = await GetTrafficAsync(traffic.Id);
            if (existing != null)
            {
                return await _database.UpdateAsync(traffic);
            }
            else
            {
                return await _database.InsertAsync(traffic);
            }
        }
        
        public Task<int> DeleteTrafficAsync(Traffic traffic)
        {
            return _database.DeleteAsync(traffic);
        }
        
        public Task DeleteAllTrafficAsync()
        {
            return _database.DeleteAllAsync<Traffic>();
        }
    }
}