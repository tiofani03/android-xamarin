using System;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using productDemo.DI;
using Xamarin.Essentials;
using Debug = System.Diagnostics.Debug;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace productDemo.Feature.Product.pages.list
{
    [Activity(
        Label = "Product List",
        Theme = "@style/AppTheme.NoActionBar")]
    public class ProductActivity : AppCompatActivity
    {
        private ProgressBar _pbLoading;
        private ProductViewModel _productViewModel;
        private RecyclerView _rvMain;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_product);

            DependencyConfig.Configure();
            _productViewModel = DependencyConfig.Container.GetInstance<ProductViewModel>();

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            _rvMain = FindViewById<RecyclerView>(Resource.Id.rvProduct);
            _pbLoading = FindViewById<ProgressBar>(Resource.Id.pbLoading);
            SetSupportActionBar(toolbar);
            LoadData();
        }

        private async Task LoadData()
        {
            try
            {
                var products = await _productViewModel.LoadProduct();
                var mainAdapter = new ProductAdapter(this, products);

                if (products.Count != 0)
                {
                    _rvMain.Visibility = ViewStates.Visible;
                    _pbLoading.Visibility = ViewStates.Gone;
                }
                else
                {
                    _rvMain.Visibility = ViewStates.Gone;
                    _pbLoading.Visibility = ViewStates.Visible;
                }

                _rvMain.SetLayoutManager(new StaggeredGridLayoutManager(2, StaggeredGridLayoutManager.Vertical));
                _rvMain.SetAdapter(mainAdapter);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: di activity" + ex.Message);
            }
        }
    }
}