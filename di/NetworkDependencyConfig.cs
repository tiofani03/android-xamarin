using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using productDemo.Data.Product.implementation.remote.api;
using Refit;
using SimpleInjector;
using Microsoft.Extensions.Logging;
using productDemo.Data.Chucker.api;
using productDemo.Data.Movie.impl.remote.api;

namespace productDemo.DI
{
    public class NetworkDependencyConfig
    {
        public static void ConfigureNetworkConfig(Container container)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://dummyjson.com");
            
            var httpClientHandler = new HttpClientHandler();
            var loggingHandler = new ChuckerInterceptor();
            loggingHandler.InnerHandler = httpClientHandler;

            var httpClientMovie = new HttpClient(loggingHandler)
            {
                BaseAddress = new Uri("https://api.themoviedb.org/3")
            };
            
            container.RegisterInstance(RestService.For<IProductService>(httpClient));
            container.RegisterInstance(RestService.For<IMovieService>(httpClientMovie));
        }
    }
    
    
    
    public class LoggingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                Console.WriteLine($"Chucker Request: {request.Method} {request.RequestUri} {request}");
                var response = await base.SendAsync(request, cancellationToken);
                stopwatch.Stop();
                var responseTimeMs = stopwatch.ElapsedMilliseconds;
                var headers =  response.Headers;
                var statusCode = (int)response.StatusCode;
                var version = response.Version;
                var content = await response.Content.ReadAsStringAsync();
                var requestMessage = response.RequestMessage;
                
                // Console.WriteLine($"Chucker Response: {statusCode} {version} {headers} {content} {requestMessage}");
                Console.WriteLine($"Chucker Response response: {response}");
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}