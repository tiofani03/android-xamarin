using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using Google.Android.Material.FloatingActionButton;
using productDemo.DI;
using productDemo.Feature.Product.pages;
using productDemo.Feature.Product.pages.list;
using productDemo.Feature.Todo.add;
using Xamarin.Essentials;
using Debug = System.Diagnostics.Debug;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace productDemo.Feature.Todo.main
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class MainActivity : AppCompatActivity
    {
        private const int UpdateRequestCode = 200;
        
        private MainViewModel _mainViewModel;
        private RecyclerView _rvMain;
        private TextView _tvHelloWorld;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            
            _mainViewModel = DependencyConfig.Container.GetInstance<MainViewModel>();

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            _tvHelloWorld = FindViewById<TextView>(Resource.Id.tvHelloWorld);
            _rvMain = FindViewById<RecyclerView>(Resource.Id.rvMain);
            SetSupportActionBar(toolbar);


            var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;
            LoadData();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            var id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                var intent = new Intent(this, typeof(ProductActivity));
                StartActivity(intent);
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            var intent = new Intent(this, typeof(AddActivity));
            StartActivityForResult(intent, UpdateRequestCode);
        }

        private async Task LoadData()
        {
            try
            {
                var todos = await _mainViewModel.LoadData();
                var mainAdapter = new MainAdapter(this, todos);

                if (todos.Count != 0)
                {
                    _rvMain.Visibility = ViewStates.Visible;
                    _tvHelloWorld.Visibility = ViewStates.Gone;
                }
                else
                {
                    _rvMain.Visibility = ViewStates.Gone;
                    _tvHelloWorld.Visibility = ViewStates.Visible;
                }

                _rvMain.SetLayoutManager(new LinearLayoutManager(this));
                _rvMain.SetAdapter(mainAdapter);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: di activity" + ex.Message);
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == UpdateRequestCode)
            {
                LoadData();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}