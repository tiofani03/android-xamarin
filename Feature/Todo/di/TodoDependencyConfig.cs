using productDemo.Feature.Todo.add;
using productDemo.Feature.Todo.detail;
using productDemo.Feature.Todo.main;
using SimpleInjector;

namespace productDemo.Feature.Todo.Di
{
    public class TodoDependencyConfig
    {
        public static void ConfigureViewModels(Container container)
        {
            container.Register<MainViewModel>(Lifestyle.Singleton);
            container.Register<DetailViewModel>(Lifestyle.Singleton);
            container.Register<AddViewModel>(Lifestyle.Singleton);
        }
    }
}