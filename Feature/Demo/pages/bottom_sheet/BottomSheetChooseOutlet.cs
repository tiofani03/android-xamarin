using System;
using System.Collections.Generic;
using System.Linq;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using Google.Android.Material.BottomSheet;

namespace productDemo.Feature.Demo.pages.bottom_sheet
{

    public interface IBottomSheet
    {
        public void OnGetDataClicked(List<ParentItem> uiData, List<ParentItem> selectedData);
    }
    public class BottomSheetChooseOutlet: BottomSheetDialogFragment, IListenCheckBoxClicked
    {
        private List<ParentItem> data { get; set;}
        private ParentAdapter parentAdapter;
        private IBottomSheet listener;

        private RecyclerView _recyclerView;
        private TextView _tvSelectAll;
        private Button _btnGetData;
        
        private bool isSelectAll = false;
        public BottomSheetChooseOutlet(List<ParentItem> generateDummyData, IBottomSheet listener)
        {
            data = Helper.DeepCopy(generateDummyData);
            this.listener = listener;
            System.Diagnostics.Debug.WriteLine("ValueIOF ParentAdapter: " + data);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.bottom_sheet_choose_outlet, container, false);
            return view;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            parentAdapter = new ParentAdapter();
            
            _recyclerView = view.FindViewById<RecyclerView>(Resource.Id.rvCheckbox);
            _tvSelectAll = view.FindViewById<TextView>(Resource.Id.tvSelectAll);
            _btnGetData = view.FindViewById<Button>(Resource.Id.btnGetData);
            
            _recyclerView.SetLayoutManager(new LinearLayoutManager(Context));
            parentAdapter.setData(new List<ParentItem>(data));
            parentAdapter.setListener(this);
            _recyclerView.SetAdapter(parentAdapter);
            
            _btnGetData.Click += delegate
            {
                listener.OnGetDataClicked(parentAdapter.GetUiData(), parentAdapter.GetData());
                Dismiss();
            };
            
            _tvSelectAll.Click += (sender, e) =>
            {
                isSelectAll = !isSelectAll; // Toggle status Select All / Unselect All
                parentAdapter.SelectAll(isSelectAll); // Setiap kali tombol diklik, panggil metode SelectAll pada parentAdapter

                // Update teks tombol berdasarkan status saat ini
                _tvSelectAll.Text = isSelectAll ? "Unselect All" : "Select All";
            };
            
        }

        public void OnParentCheckBoxClicked(List<ParentItem> items)
        {
            var allChecked = true;
            foreach (var parentItem in items)
            {
                // Check parent item
                if (!parentItem.IsChecked)
                {
                    allChecked = false;
                    break;
                }

                // Check child items
                if (parentItem.ChildItems.Any(childItem => !childItem.IsChecked))
                {
                    allChecked = false;
                }
            }

            if (allChecked)
            {
                _tvSelectAll.Text = "Unselect All";
                isSelectAll = true;
            }
            else
            {
                _tvSelectAll.Text = "Select All";
                isSelectAll = false;
            }
        }
        
        private void OnSelectAllClicked()
        {
            isSelectAll = !isSelectAll;
            parentAdapter.SelectAll(isSelectAll);
        }
    }
    
    
}

[Serializable]
public class ParentItem {
    public string Name { get; set; }
    public bool IsChecked { get; set; }
    public List<ChildItem> ChildItems { get; set; }
}

[Serializable]
public class ChildItem {
    public string Name { get; set; }
    public bool IsChecked { get; set; }
}