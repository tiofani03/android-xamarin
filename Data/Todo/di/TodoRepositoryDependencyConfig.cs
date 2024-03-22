using productDemo.Data.Todo.api.repo;
using productDemo.Data.Todo.implementation.repository;
using SimpleInjector;

namespace productDemo.Data.Todo.Di
{
    public class TodoRepositoryDependencyConfig
    {
            public static void ConfigureRepository(Container container)
            {
                container.Register<ITodoRepository, TodoRepository>(Lifestyle.Singleton);
            }
    }
}