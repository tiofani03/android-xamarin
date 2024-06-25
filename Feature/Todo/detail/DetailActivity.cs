using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Java.Lang;
using productDemo.DI;
using productDemo.Feature.Todo.add;
using productDemo.Feature.Todo.main;
using Xamarin.Essentials;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace productDemo.Feature.Todo.detail
{
    [Activity(
        Label = "Detail Activity",
        Theme = "@style/AppTheme.NoActionBar",
        ParentActivity = typeof(MainActivity))
    ]
    public class DetailActivity : AppCompatActivity
    {
        private Button _btnDelete;
        private Button _btnEdit;
        private DetailViewModel _detailViewModel;

        private Toolbar _toolbar;

        private TextView _tvDate;
        private TextView _tvDesc;
        private TextView _tvId;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_detail);
            DependencyConfig.Configure(this);
            _detailViewModel = DependencyConfig.Container.GetInstance<DetailViewModel>();

            BindItem();
            var id = Intent.GetIntExtra("EXTRA_ID", -1);
            if (id != -1) GetDetail(id);
        }

        private void BindItem()
        {
            _tvId = FindViewById<TextView>(Resource.Id.tvId);
            _tvDesc = FindViewById<TextView>(Resource.Id.tvDescription);
            _tvDate = FindViewById<TextView>(Resource.Id.tvDate);
            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

            _btnDelete = FindViewById<Button>(Resource.Id.btnDelete);
            _btnEdit = FindViewById<Button>(Resource.Id.btnEdit);

            SetSupportActionBar(_toolbar);
            SupportActionBar.Title = "Detail";
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
        }

        private async Task GetDetail(int id)
        {
            try
            {
                var detail = await _detailViewModel.GetDataById(id);
                _tvId.Text = "ID : " + detail.ID;
                _tvDesc.Text = detail.Text;
                _tvDate.Text = detail.Date.ToString("HH:mm dd MMM yyyy");

                _btnDelete.Click += (sender, args) =>
                {
                    _detailViewModel.DeleteData(detail);
                    Finish();
                };

                var intent = new Intent(this, typeof(AddActivity));
                intent.PutExtra("EXTRA_ID", detail.ID);

                _btnEdit.Click += delegate
                {
                    StartActivity(intent);
                };

                // _btnEdit.Click += (sender, args) => { StartActivityForResult(intent, 200); };
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "data tidak ditemukan " + ex, ToastLength.Long)?.Show();
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode != 200) return;
            var id = Intent.GetIntExtra("EXTRA_ID", -1);
            if (id != -1) GetDetail(id);
        }

        public override bool OnSupportNavigateUp()
        {
            Finish();
            return base.OnSupportNavigateUp();
        }
    }

    public class MyClickListener : Object, View.IOnClickListener
    {
        public void OnClick(View v)
        {
            // Kode untuk menangani klik tombol disini
        }
    }
}