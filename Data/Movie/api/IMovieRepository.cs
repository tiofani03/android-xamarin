using System.Collections.Generic;
using System.Threading.Tasks;
using productDemo.Data.Core;
using productDemo.Data.Movie.impl.remote.model;

namespace productDemo.Data.Movie.api
{
    public interface IMovieRepository
    {
        Task<MovieResponse.Movie> GetMovies(int page = 2);
        
        Task<State<MovieResponse.Movie>> GetMoviesNew(int page = 2);
        
        public Task<bool> TestConnectionLocal();
    }
}