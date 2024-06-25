using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using productDemo.DI;
using productDemo.Feature.Chucker.list;
using productDemo.Feature.Demo.pages.demo;
using productDemo.Feature.Product.pages.list;
using productDemo.Feature.Todo.main;
using Xamarin.Essentials;

namespace productDemo.Feature.Demo.pages
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainDemoActivity : AppCompatActivity
    {

        private Button _btnProduct;
        private Button _btnNote;
        private Button _btnUi;
        private Button _btnChucker;
        private MainDemoViewModel _mainDemoViewModel;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main_demo);
            
            _mainDemoViewModel = DependencyConfig.Container.GetInstance<MainDemoViewModel>();

            // DependencyConfig.Configure(this);
            _btnProduct = FindViewById<Button>(Resource.Id.btnProduct);
            _btnNote = FindViewById<Button>(Resource.Id.btnNote);
            _btnUi = FindViewById<Button>(Resource.Id.btnUiDemo);
            _btnChucker = FindViewById<Button>(Resource.Id.btnChucker);
            
            // var greeter = new Greeter(this.Application);
            // greeter.Init();


            _btnProduct.Click += async (sender, args) =>
            {
                var isConnectToServer = await  _mainDemoViewModel.TestConnection();
                if (isConnectToServer)
                {
                    var intent = new Intent(this, typeof(ProductActivity));
                    StartActivity(intent);   
                }
            };
            
            _btnNote.Click += delegate
            {
                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };

            _btnUi.Click += delegate
            {
                var intent = new Intent(this, typeof(DemoActivity));
                StartActivity(intent);
            };
            
            _btnChucker.Click += delegate
            {
                var intent = new Intent(this, typeof(ListChuckerActivity));
                StartActivity(intent);
            };


        }
    }
}