using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using productDemo.DI;
using productDemo.Feature.Todo.main;
using Xamarin.Essentials;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace productDemo.Feature.Todo.add
{
    [Activity(
        Label = "AddActivity Activity",
        Theme = "@style/AppTheme.NoActionBar",
        ParentActivity = typeof(MainActivity))
    ]
    public class AddActivity : AppCompatActivity
    {
        private AddViewModel _addViewModel;

        private EditText _edtText;

        private Toolbar _toolbar;
        private Button btnSave;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_add);
            DependencyConfig.Configure();

            _addViewModel = DependencyConfig.Container.GetInstance<AddViewModel>();
            BindItem();
            CheckUpdate();
        }

        private async Task CheckUpdate()
        {
            var id = Intent.GetIntExtra("EXTRA_ID", -1);
            if (id != -1)
            {
                var note = await _addViewModel.GetDataById(id);
                _edtText.Text = note.Text;
                
                SupportActionBar.Title = "Edit Data";
            }
            
            
            btnSave.Click += (sender, args) =>
            {
                var text = _edtText.Text;
                var todo = new Data.Todo.implementation.local.Todo
                {
                    ID = id,
                    Date = DateTime.UtcNow,
                    Text = text
                };

                _addViewModel.InsertData(todo);
                
                var intent = new Intent();
                intent.PutExtra("EXTRA_ID", id);
                SetResult(Result.Ok, intent);
                Finish();
            };
        }

        private void BindItem()
        {
            _edtText = FindViewById<EditText>(Resource.Id.edtText);
            btnSave = FindViewById<Button>(Resource.Id.btnSave);
            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

            SetSupportActionBar(_toolbar);
            SupportActionBar.Title = "Add Data";
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            
        }
    }
}