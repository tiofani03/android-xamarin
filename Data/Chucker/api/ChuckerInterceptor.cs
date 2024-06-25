using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using productDemo.Data.Chucker.api.model;
using productDemo.Data.Chucker.api.repo;
using productDemo.DI;
using productDemo.Feature.Product.pages.list;
using SimpleInjector;

namespace productDemo.Data.Chucker.api
{
    public class ChuckerInterceptor : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var chuckerRepository = DependencyConfig.Container.GetInstance<IChuckerRepository>();
            var stopwatch = Stopwatch.StartNew();
            try
            {
                var traffic = new Traffic
                {
                    Id = GenerateUniqueId(),
                    Method = request.Method.ToString(),
                    BaseUrl = request.RequestUri.Host,
                    FullUrl = request.RequestUri.AbsoluteUri,
                    Path = request.RequestUri.PathAndQuery,
                    RequestDate = DateTime.Now,
                };
                
                chuckerRepository.SaveTraffic(traffic);
                var response = await base.SendAsync(request, cancellationToken);
                stopwatch.Stop();
                traffic.StatusCode = (int)response.StatusCode;
                traffic.Response = await response.Content.ReadAsStringAsync();
                traffic.TookMs = stopwatch.ElapsedMilliseconds;
                traffic.ResponseDate = DateTime.Now.ToString();
                
                chuckerRepository.UpdateTraffic(traffic);
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        private string GenerateUniqueId()
        {
            var timestamp = DateTime.Now.Ticks.ToString(); // Menggunakan ticks sebagai timestamp
            var guid = Guid.NewGuid().ToString(); // Menggunakan GUID sebagai nilai unik
            return timestamp + "_" + guid;
        }
    }
}