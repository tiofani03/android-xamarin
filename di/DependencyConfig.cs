using SimpleInjector;

namespace productDemo.DI
{
    public static class DependencyConfig
    {
        public static Container Container { get; private set; }

        public static void Configure()
        {
            Container = new Container();

            // Register dependencies
            DataDependencyConfig.ConfigureDataLayer(Container);
            FeatureDependencyConfig.ConfigureFeatureLayer(Container);
            NetworkDependencyConfig.ConfigureNetworkConfig(Container);

            Container.Verify();
        }
    }
}