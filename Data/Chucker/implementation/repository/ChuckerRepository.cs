using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using productDemo.Data.Chucker.api.model;
using productDemo.Data.Chucker.api.repo;

namespace productDemo.Data.Chucker.implementation.repository
{
    public class ChuckerRepository : IChuckerRepository
    {
        private static ChuckerDatabase _chuckerDatabase;

        public static ChuckerDatabase Database
        {
            get
            {
                if (_chuckerDatabase == null)
                    _chuckerDatabase = new ChuckerDatabase(Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Chucker.db3"));

                return _chuckerDatabase;
            }
        }
        
        public Task SaveTraffic(Traffic traffic)
        {
            return Database.SaveTrafficAsync(traffic);
        }

        public Task UpdateTraffic(Traffic traffic)
        {
            return Database.SaveTrafficAsync(traffic);
        }

        public Task<List<Traffic>> ReadTraffic()
        {
            return Database.GetTrafficAsync();
        }

        public Task<Traffic> GetTrafficById(string id)
        {
            return Database.GetTrafficAsync(id);
        }

        public Task DeleteTraffic(Traffic traffic)
        {
            return Database.DeleteTrafficAsync(traffic);
        }

        public Task DeleteAllTraffic()
        {
            return Database.DeleteAllTrafficAsync();
        }
    }
}