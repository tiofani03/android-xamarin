using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using Bumptech.Glide;

namespace productDemo.Feature.Product.pages.list
{
    public class ProductAdapter : RecyclerView.Adapter
    {
        private List<Data.Product.api.model.Product> items;
        private Context context;
        
        public ProductAdapter(Context context, List<Data.Product.api.model.Product> items)
        {
            this.context = context;
            this.items = items;
        }
        
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var viewHolder = holder as ItemViewHolder;
            viewHolder.BindItem(items[position]);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context)?.Inflate(Resource.Layout.item_product, parent, false);
            var viewHolder = new ItemViewHolder(itemView);
            return viewHolder;
        }

        public override int ItemCount => items.Count;
    }
    
    public class ItemViewHolder : RecyclerView.ViewHolder
    {
        private TextView _tvTitle;
        private TextView _tvRealPrice;
        private TextView _tvPrice;
        private TextView _tvDiscount;

        private ImageView _ivProduct;

        public ItemViewHolder(View itemView) : base(itemView)
        {
            _tvTitle = itemView.FindViewById<TextView>(Resource.Id.tvProductName);
            _tvRealPrice = itemView.FindViewById<TextView>(Resource.Id.tvProductRealPrice);
            _tvPrice = itemView.FindViewById<TextView>(Resource.Id.tvProductPrice);
            _tvDiscount = itemView.FindViewById<TextView>(Resource.Id.tvDiscount);

            _ivProduct = itemView.FindViewById<ImageView>(Resource.Id.ivProduct);
        }

        public void BindItem(Data.Product.api.model.Product item)
        {
            var discountedPrice = (int)(item.Price - (item.DiscountPercentage / 100) * item.Price);
            
            _tvTitle.Text = item.Title;
            _tvRealPrice.Text = "$" + discountedPrice;
            _tvPrice.Text = "$" + item.Price;
            _tvPrice.PaintFlags |= Android.Graphics.PaintFlags.StrikeThruText;
            
            _tvDiscount.Text = item.DiscountPercentage + "%";

            Glide.With(ItemView.Context)
                .Load(item.Thumbnail)
                .Into(_ivProduct);
        }
    }
}