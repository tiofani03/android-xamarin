using System.Collections.Generic;
using System.Linq;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;

namespace productDemo.Feature.Demo.pages.bottom_sheet
{
    public class ChildAdapter : RecyclerView.Adapter
    {
        private List<ChildItem> items { get; set; }
        private ParentAdapter parentAdapter;
        private int parentPosition;

        public override int ItemCount => items.Count;

        public void setData(List<ChildItem> items, ParentAdapter parentAdapter)
        {
            this.items = new List<ChildItem>(items);
            this.parentAdapter = parentAdapter;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var viewHolder = holder as ChildViewHolder;
            viewHolder.bindItem(items[position]);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context)?.Inflate(Resource.Layout.item_check_box, parent, false);
            var viewHolder = new ChildViewHolder(itemView, parentAdapter, parentPosition);
            return viewHolder;
        }

        public void setParentPosition(int adapterPosition)
        {
            parentPosition = adapterPosition;
        }
    }

    public class ChildViewHolder : RecyclerView.ViewHolder
    {
        private ParentAdapter parentAdapter;
        public ChildViewHolder(View itemView,  ParentAdapter parentAdapter, int parentPosition) : base(itemView)
        {
            TvName = itemView.FindViewById<TextView>(Resource.Id.title);
            CheckBox = itemView.FindViewById<CheckBox>(Resource.Id.checkbox);
            
            this.parentAdapter = parentAdapter;
            CheckBox.Click += (sender, e) =>
            {
                var childItem = parentAdapter.GetParentItem(parentPosition).ChildItems[AdapterPosition];
                childItem.IsChecked = CheckBox.Checked;
                this.parentAdapter.TriggerCheckBoxClicked();
                
                var allChildChecked = parentAdapter.GetParentItem(parentPosition).ChildItems.All(child => child.IsChecked);
                parentAdapter.GetParentItem(parentPosition).IsChecked = allChildChecked;
                parentAdapter.NotifyDataSetChanged();
            };
            
        }

        public TextView TvName { get; }
        public CheckBox CheckBox { get; }

        public void bindItem(ChildItem item)
        {
            TvName.Text = item.Name;
            CheckBox.Checked = item.IsChecked;
        }
    }
}