using productDemo.Data.Product.api.repo;
using productDemo.Data.Product.implementation.repo;
using SimpleInjector;

namespace productDemo.Data.Product.di
{
    public class ProductRepositoryDependencyConfig
    {
        public static void ConfigureRepository(Container container)
        {
            container.Register<IProductRepository, ProductRepository>(Lifestyle.Singleton);
        }
    }
}