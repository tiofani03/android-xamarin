using System.Collections.Generic;
using System.Linq;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;

namespace productDemo.Feature.Demo.pages.bottom_sheet
{
    public interface IListenCheckBoxClicked
    {
        public void OnParentCheckBoxClicked(List<ParentItem> items);
    }
    public class ParentAdapter : RecyclerView.Adapter
    {
        private List<ParentItem> items { get; set;}
        private IListenCheckBoxClicked listener;

        public override int ItemCount => items.Count;
        
        public void setListener(IListenCheckBoxClicked listener)
        {
            this.listener = listener;
        }

        public void setData(List<ParentItem> items)
        {
            this.items = new List<ParentItem>(items);
            System.Diagnostics.Debug.WriteLine("ValueIOF ParentAdapter: " + items);
        }

        public List<ParentItem> GetUiData()
        {
            return items;
        }
        
        public List<ParentItem> GetData()
        {
            var selectedData = new List<ParentItem>();

            foreach (var parentItem in items)
            {
                if (parentItem.IsChecked || parentItem.ChildItems.Any(childItem => childItem.IsChecked))
                {
                    // Create a copy of the parent item
                    var selectedParent = new ParentItem
                    {
                        Name = parentItem.Name,
                        IsChecked = parentItem.IsChecked,
                        ChildItems = new List<ChildItem>()
                    };

                    // Add selected child items to the selected parent
                    foreach (var childItem in parentItem.ChildItems)
                    {
                        if (childItem.IsChecked)
                        {
                            selectedParent.ChildItems.Add(childItem);
                        }
                    }

                    // Add selected parent to the list
                    selectedData.Add(selectedParent);
                }
            }

            return selectedData;
        }


        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var viewHolder = holder as ParentViewHolder;
            viewHolder.bindItem(items[viewHolder.AdapterPosition]);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context)?.Inflate(Resource.Layout.item_check_box, parent, false);
            var viewHolder = new ParentViewHolder(itemView, this);
            return viewHolder;
        }
        
        public void TriggerCheckBoxClicked()
        {
            listener.OnParentCheckBoxClicked(GetUiData());
        }
        
        public void OnParentCheckBoxClicked(int position, bool isChecked)
        {
            var parentItem = items[position];
            parentItem.IsChecked = isChecked;
            foreach (var childItem in parentItem.ChildItems)
            {
                childItem.IsChecked = isChecked;
            }

            TriggerCheckBoxClicked();
            NotifyDataSetChanged();
        }
        
        public ParentItem GetParentItem(int position)
        {
            return items[position];
        }
        
        public void SelectAll(bool state)
        {
            // Set CheckBox status for all parent items and child items based on the 'state' parameter
            foreach (var parentItem in items)
            {
                parentItem.IsChecked = state;
                foreach (var childItem in parentItem.ChildItems)
                {
                    childItem.IsChecked = state;
                }
            }

            NotifyDataSetChanged();
        }
    }

    public class ParentViewHolder : RecyclerView.ViewHolder
    {
        private ParentAdapter parentAdapter;
        public ParentViewHolder(View itemView, ParentAdapter parentAdapter) : base(itemView)
        {
            TvName = itemView.FindViewById<TextView>(Resource.Id.title);
            RvChild = itemView.FindViewById<RecyclerView>(Resource.Id.rvChild);
            CheckBox = itemView.FindViewById<CheckBox>(Resource.Id.checkbox);
            
            this.parentAdapter = parentAdapter;
            
            CheckBox.Click += (sender, e) => {
                parentAdapter.OnParentCheckBoxClicked(AdapterPosition, CheckBox.Checked);
            };
        }

        public CheckBox CheckBox { get; }
        public TextView TvName { get; }
        public RecyclerView RvChild { get; }

        public void bindItem(ParentItem item)
        {
            TvName.Text = item.Name;
            var childAdapter = new ChildAdapter();
            CheckBox.Checked = item.IsChecked;
            
            childAdapter.setData(item.ChildItems, parentAdapter);
            childAdapter.setParentPosition(AdapterPosition);
            RvChild.SetLayoutManager(new LinearLayoutManager(ItemView.Context));
            RvChild.SetAdapter(childAdapter);
        }
    }
}