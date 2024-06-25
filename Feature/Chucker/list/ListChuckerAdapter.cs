using System.Collections.Generic;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using productDemo.Data.Chucker.api.model;

namespace productDemo.Feature.Chucker.list
{
    public class ListChuckerAdapter : RecyclerView.Adapter
    {
        
        private List<Traffic> items;
        
        public ListChuckerAdapter(List<Traffic> items)
        {
            this.items = items;
        }
        
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var viewHolder = holder as ItemViewHolder;
            var item = items[position];
            
            viewHolder.TvCode.Text = item.StatusCode.ToString();
            viewHolder.TvPath.Text = item.Method +"  "+ item.Path;
            viewHolder.TvHost.Text = item.BaseUrl;
            var dateString1 = item.RequestDate.ToString("HH.mm.ss");
            viewHolder.TvTimeStart.Text = dateString1;
            viewHolder.TvTimeDuration.Text = item.TookMs + " ms";
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context)?.Inflate(Resource.Layout.item_chucker_transaction, parent, false);
            var viewHolder = new ItemViewHolder(itemView);
            return viewHolder;
        }

        public override int ItemCount => items.Count;
    }
    
    public class ItemViewHolder : RecyclerView.ViewHolder
    {
        public TextView TvCode { get; private set; }
        public TextView TvPath { get; private set; }
        public TextView TvHost { get; private set; }
        public TextView TvTimeStart { get; private set; }
        public TextView TvTimeDuration { get; private set; }
        public View ViewSeparator { get; private set; }

        public ItemViewHolder(View itemView) : base(itemView)
        {
            TvCode = itemView.FindViewById<TextView>(Resource.Id.code);
            TvPath = itemView.FindViewById<TextView>(Resource.Id.path);
            TvHost = itemView.FindViewById<TextView>(Resource.Id.host);
            TvTimeStart = itemView.FindViewById<TextView>(Resource.Id.timeStart);
            TvTimeDuration = itemView.FindViewById<TextView>(Resource.Id.duration);
            ViewSeparator = itemView.FindViewById<View>(Resource.Id.viewSeparator);
        }
    }
}