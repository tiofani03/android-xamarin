using System;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Views;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using productDemo.DI;
using Xamarin.Essentials;

namespace productDemo.Feature.Chucker.list
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class ListChuckerActivity : AppCompatActivity
    {
        
        private ListChuckerViewModel _listChuckerViewModel;
        private RecyclerView _rvChucker;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_list_chucker);
            
            _listChuckerViewModel = DependencyConfig.Container.GetInstance<ListChuckerViewModel>();
            _rvChucker = FindViewById<RecyclerView>(Resource.Id.rvChucker);
            LoadData();
        }

        private async Task LoadData()
        {
            try
            {
                var todos = await _listChuckerViewModel.LoadData();
                var mainAdapter = new ListChuckerAdapter(todos);
                if (todos.Count != 0)
                {
                    _rvChucker.Visibility = ViewStates.Visible;
                }
                else
                {
                    _rvChucker.Visibility = ViewStates.Gone;
                }

                _rvChucker.SetLayoutManager(new LinearLayoutManager(this));
                _rvChucker.SetAdapter(mainAdapter);

               
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Data loaded: " + ex.Message);
                Console.WriteLine(ex);
            }
        }
    }
}