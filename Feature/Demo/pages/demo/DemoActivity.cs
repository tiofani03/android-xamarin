using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using productDemo.Feature.Demo.pages.bottom_sheet;
using Xamarin.Essentials;

namespace productDemo.Feature.Demo.pages.demo
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    // [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class DemoActivity : AppCompatActivity, IBottomSheet
    {
        private Button _btnBottomSheet;
        private Button _btnDownload;
        private Button _btnStickyBottomSheet;
        private List<ParentItem> data { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_demo);

            data = new List<ParentItem>(GenerateDummyData());
            _btnBottomSheet = FindViewById<Button>(Resource.Id.btnBottomSheet);
            _btnDownload = FindViewById<Button>(Resource.Id.btnDownload);
            _btnStickyBottomSheet = FindViewById<Button>(Resource.Id.btnStickyBottomSheet);
            
            var copyData = Helper.DeepCopy(data);

            _btnBottomSheet.Click += delegate
            {
                System.Diagnostics.Debug.WriteLine("ValueIOF ParentAdapter: " + data);
                var bottomSheetDialog = new BottomSheetChooseOutlet(copyData, this);
                bottomSheetDialog.Show(SupportFragmentManager, bottomSheetDialog.Tag);
            };

            _btnDownload.Click += async delegate
            {
                string url = "https://pii.or.id/uploads/dummies.pdf"; // URL file yang ingin Anda unduh
                string filePath = "downloadedFile.pdf"; // Lokasi file yang akan disimpan di perangkat

                FileDownloader downloader = new FileDownloader();
                await downloader.DownloadFile(url, filePath);
            };
            
            _btnStickyBottomSheet.Click += delegate
            {
                var bottomSheetDialog = new BottomSheetChooseOutlet(copyData, this);
                bottomSheetDialog.Show(SupportFragmentManager, bottomSheetDialog.Tag);
            };
        }
        
        // generate dummy data
        private List<ParentItem> GenerateDummyData() {
            var parentItems = new List<ParentItem>();

            // Generate parent items
            for (int i = 0; i < 20; i++) {
                ParentItem parentItem = new ParentItem {
                    Name = "Parent " + i,
                    IsChecked = false,
                    ChildItems = new List<ChildItem>()
                };
                
                if (i == 1 || i == 3) // Contoh: Check parent items kedua dan keempat
                {
                    parentItem.IsChecked = true;
                }

                // Generate child items for each parent
                for (int j = 0; j < 3; j++) {
                    ChildItem childItem = new ChildItem {
                        Name = "Child " + j + " of Parent " + i,
                        IsChecked = false
                    };
                    
                    if (i == 1 && (j == 0 || j == 2)) // Contoh: Check child items untuk parent kedua
                    {
                        childItem.IsChecked = true;
                    }
                    parentItem.ChildItems.Add(childItem);
                }
                
                

                parentItems.Add(parentItem);
            }
            
            parentItems.Add(new ParentItem
            {
                Name = "Parent without child",
                IsChecked = false,
                ChildItems = new List<ChildItem>() // No child items
            });

            return parentItems;
        }

        public void OnGetDataClicked(List<ParentItem> uiData, List<ParentItem> selectedData)
        {
            data = new List<ParentItem>(uiData);
        }
    }
    
    public class FileDownloader
    {
        public async Task DownloadFile(string url, string filePath)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    await client.DownloadFileTaskAsync(new Uri(url), filePath);
                    Console.WriteLine("File downloaded successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error downloading file: " + ex.Message);
            }
        }
    }
}
