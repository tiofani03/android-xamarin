using productDemo.Feature.Chucker.list;
using SimpleInjector;

namespace productDemo.Feature.Chucker.di
{
    public class ChuckerDependencyConfig
    {
        public static void ConfigureViewModels(Container container)
        {
            container.Register<ListChuckerViewModel>(Lifestyle.Singleton);
        }
    }
}