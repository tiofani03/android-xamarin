using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using productDemo.Data.Chucker.api.model;
using productDemo.Data.Chucker.implementation;
using Xamarin.Forms.Shapes;
using Path = System.IO.Path;

namespace productDemo.Data.Chucker.api.repo
{
    public interface IChuckerRepository
    {
        public Task SaveTraffic(Traffic traffic);
        public Task UpdateTraffic(Traffic traffic);
        public Task<List<Traffic>> ReadTraffic();
        public Task<Traffic> GetTrafficById(string id);
        public Task DeleteTraffic(Traffic traffic);
        public Task DeleteAllTraffic();
    }
}