using System;
using System.Threading.Tasks;
using AndroidX.Lifecycle;
using productDemo.Data.Movie.api;

namespace productDemo.Feature.Demo.pages
{
    public class MainDemoViewModel : ViewModel
    {
        private readonly IMovieRepository _movieRepository;
        
        public MainDemoViewModel(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        
        public async Task<bool> TestConnection()
        {
            var isConnected = await _movieRepository.TestConnectionLocal();
            return isConnected;
        }
    }
}