using productDemo.Data.Movie.api;
using productDemo.Data.Movie.impl.repo;
using SimpleInjector;

namespace productDemo.Data.Movie.di
{
    public class MovieRepositoryDependencyConfig
    {
        public static void ConfigureRepository(Container container)
        {
            container.Register<IMovieRepository, MovieRepository>(Lifestyle.Singleton);
        }
    }
}