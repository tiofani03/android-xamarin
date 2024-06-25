using System;
using System.Net.Http;
using System.Threading.Tasks;
using Android.Util;
using productDemo.Data.Core;
using productDemo.Data.Movie.api;
using productDemo.Data.Movie.impl.remote.api;
using productDemo.Data.Movie.impl.remote.model;

namespace productDemo.Data.Movie.impl.repo
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IMovieService _movieService;

        public MovieRepository(IMovieService movieService)
        {
            _movieService = movieService;
        }
        
        public async Task<MovieResponse.Movie> GetMovies(int page = 2)
        {
            try
            {
                var movie = await _movieService.GetMovies(page);
                return movie;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get products from the API", ex);
            }
        }

        public async Task<State<MovieResponse.Movie>> GetMoviesNew(int page = 2)
        {
            try
            {
                var movie = await _movieService.GetMovies(page);
                return State<MovieResponse.Movie>.Success(movie);
            }
            catch (Exception e)
            {
                return e.ToError<MovieResponse.Movie>();
            }
        }
        
        public async Task<bool> TestConnectionLocal()
        {
            bool isConnected;
            try
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync("http://10.118.238.89:8080");
                isConnected = response.StatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                isConnected = false;
            }
            return isConnected;
        }
    }
}