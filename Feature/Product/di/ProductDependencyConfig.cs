using productDemo.Feature.Product.pages;
using productDemo.Feature.Product.pages.list;
using SimpleInjector;

namespace productDemo.Feature.Product.di
{
    public class ProductDependencyConfig
    {
        public static void ConfigureViewModels(Container container)
        {
            container.Register<ProductViewModel>(Lifestyle.Singleton);
        }
    }
}