using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using productDemo.Feature.Todo.detail;

namespace productDemo.Feature.Todo.main
{
    public class MainAdapter : RecyclerView.Adapter
    {
        private List<Data.Todo.implementation.local.Todo> items;
        private Context context;
        
        public MainAdapter(Context context, List<Data.Todo.implementation.local.Todo> items)
        {
            this.context = context;
            this.items = items;
        }
        
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var viewHolder = holder as ItemViewHolder;
            viewHolder.TvId.Text = "ID : "+ items[position].ID;
            viewHolder.TvDescription.Text = items[position].Text;

            holder.ItemView.Click += delegate
            {
                var intent = new Intent(context, typeof(DetailActivity));
                intent.PutExtra("EXTRA_ID", items[position].ID);
                ((Activity)context).StartActivityForResult(intent, 200);
            };


        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context)?.Inflate(Resource.Layout.item_main, parent, false);
            var viewHolder = new ItemViewHolder(itemView);
            return viewHolder;
        }

        public override int ItemCount => items.Count;
    }
    
    public class ItemViewHolder : RecyclerView.ViewHolder
    {
        public TextView TvId { get; private set; }
        public TextView TvDescription { get; private set; }

        public ItemViewHolder(View itemView) : base(itemView)
        {
            TvId = itemView.FindViewById<TextView>(Resource.Id.tvId);
            TvDescription = itemView.FindViewById<TextView>(Resource.Id.tvDescription);
        }
    }
}