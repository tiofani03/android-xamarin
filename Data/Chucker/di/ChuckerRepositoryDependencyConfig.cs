using productDemo.Data.Chucker.api;
using productDemo.Data.Chucker.api.repo;
using productDemo.Data.Chucker.implementation.repository;
using SimpleInjector;

namespace productDemo.Data.Chucker.di
{
    public class ChuckerRepositoryDependencyConfig
    {
        public static void ConfigureRepository(Container container)
        {
            container.Register<IChuckerRepository, ChuckerRepository>(Lifestyle.Singleton);
        }
    }
}