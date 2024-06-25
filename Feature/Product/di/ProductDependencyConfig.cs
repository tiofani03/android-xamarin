using productDemo.Feature.Demo.pages;
using productDemo.Feature.Product.pages.list;
using SimpleInjector;

namespace productDemo.Feature.Product.di
{
    public class ProductDependencyConfig
    {
        #region Methods

        public static void ConfigureViewModels(Container container)
        {
            container.Register<ProductViewModel>(Lifestyle.Singleton);
            container.Register<MainDemoViewModel>(Lifestyle.Singleton);
        }

        #endregion
    }
}