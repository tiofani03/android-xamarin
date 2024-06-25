using productDemo.Feature.Chucker.di;
using productDemo.Feature.Product.di;
using productDemo.Feature.Todo.Di;
using SimpleInjector;

namespace productDemo.DI
{
    public class FeatureDependencyConfig
    {
        public static void ConfigureFeatureLayer(Container container)
        {
            // Register dependencies
            TodoDependencyConfig.ConfigureViewModels(container);
            ProductDependencyConfig.ConfigureViewModels(container);
            ChuckerDependencyConfig.ConfigureViewModels(container);
        }
    }
}