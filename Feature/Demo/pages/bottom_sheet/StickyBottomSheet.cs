using System.Collections.Generic;
using Android.OS;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using Google.Android.Material.BottomSheet;
using Debug = System.Diagnostics.Debug;

namespace productDemo.Feature.Demo.pages.bottom_sheet
{
    public class StickyBottomSheet : BottomSheetDialogFragment, IListenCheckBoxClicked
    {
        private RecyclerView _recyclerView;
        private IBottomSheet listener;
        private ParentAdapter parentAdapter;

        public StickyBottomSheet(List<ParentItem> generateDummyData, IBottomSheet listener)
        {
            data = Helper.DeepCopy(generateDummyData);
            this.listener = listener;
            Debug.WriteLine("ValueIOF ParentAdapter: " + data);
        }

        private List<ParentItem> data { get; }

        public void OnParentCheckBoxClicked(List<ParentItem> items)
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.bottom_sheet_sticky, container, false);
            return view;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            parentAdapter = new ParentAdapter();

            _recyclerView = view.FindViewById<RecyclerView>(Resource.Id.sheet_recyclerview);
            _recyclerView.SetLayoutManager(new LinearLayoutManager(Context));
            parentAdapter.setData(new List<ParentItem>(data));
            parentAdapter.setListener(this);
            _recyclerView.SetAdapter(parentAdapter);
        }
    }
}