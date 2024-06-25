using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using Bumptech.Glide;
using productDemo.Data.Movie.impl.remote.model;

namespace productDemo.Feature.Product.pages.list
{
    public class ProductMovieAdapter : RecyclerView.Adapter
    {
        private Context context;
        private readonly List<MovieResponse.MovieResult> items;
        public Action onCardClick;

        public ProductMovieAdapter(List<MovieResponse.MovieResult> items)
        {
            this.items = items;
        }

        public override int ItemCount => items.Count;
        
        public void SetInitialData(List<MovieResponse.MovieResult> items)
        {
            this.items.Clear();
            this.items.AddRange(items);
            NotifyDataSetChanged();
        }

        public void UpdateData(List<MovieResponse.MovieResult> items, bool shouldClear = false)
        {
            // var diffCallback = new ProductMovieDiffCallback(this.items, items);
            // var diffResult = DiffUtil.CalculateDiff(diffCallback);
            // this.items.AddRange(items);
            // diffResult.DispatchUpdatesTo(this);
            // var diffCallback = new ProductMovieDiffCallback(this.items, items);
            // var diffMovies = DiffUtil.CalculateDiff(diffCallback);
            // if (shouldClear) {
            //     this.items.Clear(); 
            // }
            this.items.AddRange(items);
            NotifyItemInserted(this.items.Count - 1);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var viewHolder = holder as MovieViewHolder;
            viewHolder.BindItem(items[position], onCardClick);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context)?.Inflate(Resource.Layout.item_product, parent, false);
            var viewHolder = new MovieViewHolder(itemView);
            return viewHolder;
        }
    }

    public class MovieViewHolder : RecyclerView.ViewHolder
    {
        private readonly ImageView _ivProduct;
        private readonly TextView _tvDiscount;
        private readonly TextView _tvPrice;
        private readonly TextView _tvRealPrice;
        private readonly TextView _tvTitle;

        public MovieViewHolder(View itemView) : base(itemView)
        {
            _tvTitle = itemView.FindViewById<TextView>(Resource.Id.tvProductName);
            _tvRealPrice = itemView.FindViewById<TextView>(Resource.Id.tvProductRealPrice);
            _tvPrice = itemView.FindViewById<TextView>(Resource.Id.tvProductPrice);
            _tvDiscount = itemView.FindViewById<TextView>(Resource.Id.tvDiscount);

            _ivProduct = itemView.FindViewById<ImageView>(Resource.Id.ivProduct);
        }

        public void BindItem(MovieResponse.MovieResult item, Action onCardClick)
        {
            // var discountedPrice = (int)(item.Price - item.DiscountPercentage / 100 * item.Price);
            //
            // _tvTitle.Text = item.Title;
            // _tvRealPrice.Text = "$" + discountedPrice;
            // _tvPrice.Text = "$" + item.Price;
            // _tvPrice.PaintFlags |= PaintFlags.StrikeThruText;
            //
            // _tvDiscount.Text = item.DiscountPercentage + "%";

            Glide.With(ItemView.Context)
                .Load("https://image.tmdb.org/t/p/w300" + item.Poster_Path)
                .Into(_ivProduct);
            
            ItemView.Click += (sender, args) =>
            {
                onCardClick?.Invoke();
            };
        }
    }

    public class ProductMovieDiffCallback : DiffUtil.Callback
    {
        private readonly List<MovieResponse.MovieResult> newList;
        private readonly List<MovieResponse.MovieResult> oldList;

        public ProductMovieDiffCallback(List<MovieResponse.MovieResult> oldList,
            List<MovieResponse.MovieResult> newList)
        {
            this.oldList = oldList;
            this.newList = newList;
        }

        public override int OldListSize => oldList.Count;

        public override int NewListSize => newList.Count;

        public override bool AreItemsTheSame(int oldItemPosition, int newItemPosition)
        {
            // Implement logic to check if the same item in old and new list
            return oldList[oldItemPosition].Id == newList[newItemPosition].Id;
        }

        public override bool AreContentsTheSame(int oldItemPosition, int newItemPosition)
        {
            // Implement logic to check if the contents of items are the same
            return oldList[oldItemPosition].Equals(newList[newItemPosition]);
        }
    }
}