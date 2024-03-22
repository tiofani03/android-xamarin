using productDemo.Data.Product.di;
using productDemo.Data.Todo.Di;
using SimpleInjector;

namespace productDemo.DI
{
    public class DataDependencyConfig
    {
        public static void ConfigureDataLayer(Container container)
        {
            TodoRepositoryDependencyConfig.ConfigureRepository(container);
            ProductRepositoryDependencyConfig.ConfigureRepository(container);
        }
    }
}