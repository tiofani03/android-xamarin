using System.Threading.Tasks;
using productDemo.Data.Movie.impl.remote.model;
using productDemo.Data.Product.implementation.remote.response;
using Refit;

namespace productDemo.Data.Movie.impl.remote.api
{
    public interface IMovieService
    {
        // [Get("/movie/popular?page?api_key=015a0e5537965d19010f5e96314052c5")]
        // Task<MovieResponse.Movie> GetMovies();
        
        // [Get("movie/popular?page={page}&api_key=015a0e5537965d19010f5e96314052c5")]
        // Task<MovieResponse.Movie> GetMovies([Query("page")] int page);
        
        
        [Get("/movie/popular?page={page}&api_key=015a0e5537965d19010f5e96314052c")]
        Task<MovieResponse.Movie> GetMovies([AliasAs("page")] int page);
        
    }
    
}