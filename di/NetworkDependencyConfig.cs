using System;
using System.Net.Http;
using productDemo.Data.Product.implementation.remote.api;
using Refit;
using SimpleInjector;
using Microsoft.Extensions.Logging;

namespace productDemo.DI
{
    public class NetworkDependencyConfig
    {
        public static void ConfigureNetworkConfig(Container container)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer YOUR_ACCESS_TOKEN");
            httpClient.BaseAddress = new Uri("https://dummyjson.com");
            
            container.RegisterInstance(RestService.For<IProductService>(httpClient));
        }
    }
}