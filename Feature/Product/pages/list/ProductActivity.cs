using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using productDemo.Data.Core;
using productDemo.Data.Movie.impl.remote.model;
using productDemo.DI;
using Xamarin.Essentials;
using Debug = System.Diagnostics.Debug;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace productDemo.Feature.Product.pages.list
{
    [Activity(
        Label = "Product List",
        Theme = "@style/AppTheme.NoActionBar")]
    public class ProductActivity : AppCompatActivity
    {
        //     private ProgressBar _pbLoading;
        //     private ProductViewModel _productViewModel;
        //     private RecyclerView _rvMain;
        //     private ProductMovieAdapter mainAdapter;
        //     private int page = 1;
        //     private int totalPages = 0;
        //
        //     protected override void OnCreate(Bundle savedInstanceState)
        //     {
        //         base.OnCreate(savedInstanceState);
        //         Platform.Init(this, savedInstanceState);
        //         SetContentView(Resource.Layout.activity_product);
        //
        //         DependencyConfig.Configure();
        //         _productViewModel = DependencyConfig.Container.GetInstance<ProductViewModel>();
        //
        //         var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
        //         _rvMain = FindViewById<RecyclerView>(Resource.Id.rvProduct);
        //         mainAdapter = new ProductMovieAdapter(new List<MovieResponse.MovieResult>());
        //         var layoutManager = new LinearLayoutManager(this);
        //         var onScrollListener = new XamarinRecyclerViewOnScrollListener (layoutManager);
        //         onScrollListener.LoadMoreEvent += (sender, e) =>
        //         {
        //             if (page < totalPages)
        //             {
        //                 LoadDataMovie(page);
        //                 _pbLoading.Visibility = ViewStates.Visible;
        //                 page++;
        //             }
        //             
        //         };
        //
        //         _rvMain.SetLayoutManager(layoutManager);
        //         _rvMain.AddOnScrollListener (onScrollListener);
        //         _rvMain.SetAdapter(mainAdapter);
        //         
        //         _pbLoading = FindViewById<ProgressBar>(Resource.Id.pbLoading);
        //         SetSupportActionBar(toolbar);
        //         LoadDataMovie(page);
        //     }
        //
        //     // private async Task LoadData()
        //     // {
        //     //     try
        //     //     {
        //     //         var products = await _productViewModel.LoadProduct();
        //     //         mainAdapter.UpdateData(products);
        //     //
        //     //         if (products.Count != 0)
        //     //         {
        //     //             _rvMain.Visibility = ViewStates.Visible;
        //     //             _pbLoading.Visibility = ViewStates.Gone;
        //     //         }
        //     //         else
        //     //         {
        //     //             _rvMain.Visibility = ViewStates.Gone;
        //     //             _pbLoading.Visibility = ViewStates.Visible;
        //     //         }
        //     //     }
        //     //     catch (Exception ex)
        //     //     {
        //     //         Debug.WriteLine("Error: di activity" + ex.Message);
        //     //     }
        //     // }
        //     
        //     private async Task LoadDataMovie(int page = 1)
        //     {
        //         try
        //         {
        //             var products = await _productViewModel.GetMovies(page);
        //             totalPages = products.Total_Pages;
        //             mainAdapter.UpdateData(products.Results);
        //
        //             if (products.Results.Count != 0 && page != totalPages)
        //             {
        //                 _rvMain.Visibility = ViewStates.Visible;
        //                 _pbLoading.Visibility = ViewStates.Gone;
        //             }
        //             else
        //             {
        //                 _rvMain.Visibility = ViewStates.Gone;
        //                 _pbLoading.Visibility = ViewStates.Visible;
        //             }
        //         }
        //         catch (Exception ex)
        //         {
        //             Debug.WriteLine("Error: di activity" + ex.Message);
        //         }
        //     }
        // }
        //
        // public class  XamarinRecyclerViewOnScrollListener : RecyclerView.OnScrollListener
        // {
        //     public delegate void LoadMoreEventHandler(object sender, EventArgs e);
        //     public event LoadMoreEventHandler LoadMoreEvent;
        //
        //     private LinearLayoutManager LayoutManager;
        //
        //     public XamarinRecyclerViewOnScrollListener (LinearLayoutManager layoutManager)
        //     {
        //         LayoutManager = layoutManager;
        //     }
        //
        //     public override void OnScrolled (RecyclerView recyclerView, int dx, int dy)
        //     {
        //         base.OnScrolled (recyclerView, dx, dy);
        //
        //         var visibleItemCount = recyclerView.ChildCount;
        //         var totalItemCount = recyclerView.GetAdapter().ItemCount;
        //         var pastVisiblesItems = LayoutManager.FindFirstVisibleItemPosition();
        //
        //         if ((visibleItemCount + pastVisiblesItems) >= totalItemCount) {
        //             LoadMoreEvent (this, null);
        //         }
        //     }
        // }

        private ProgressBar _pbLoading;
        private ProductViewModel _productViewModel;
        private RecyclerView _rvMain;
        private ProductMovieAdapter mainAdapter;
        private int page = 1;
        private int totalPages;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_product);

            // DependencyConfig.Configure(this);
            _productViewModel = DependencyConfig.Container.GetInstance<ProductViewModel>();

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            _rvMain = FindViewById<RecyclerView>(Resource.Id.rvProduct);
            mainAdapter = new ProductMovieAdapter(new List<MovieResponse.MovieResult>());
            // mainAdapter.onCardClick = () => 
            // {
            //     Toast.MakeText(this, "Card Clicked", ToastLength.Long).Show();
            // };
            var layoutManager = new LinearLayoutManager(this);
            var onScrollListener = new XamarinRecyclerViewOnScrollListener(layoutManager);
            onScrollListener.LoadMoreEvent += (sender, e) =>
            {
                if (page < totalPages)
                {
                    page++;
                    Toast.MakeText(this, "Hit ke page : " + page, ToastLength.Long).Show();
                    LoadDataMovieNews(page);
                }
            };

            _rvMain.SetLayoutManager(layoutManager);
            _rvMain.AddOnScrollListener(onScrollListener);
            _rvMain.SetAdapter(mainAdapter);

            _pbLoading = FindViewById<ProgressBar>(Resource.Id.pbLoading);
            SetSupportActionBar(toolbar);
            // LoadDataMovie(page);
            LoadDataMovieNews(page);
            _productViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(_productViewModel.MovieState) )
                {
                    HandleState(
                        _productViewModel.MovieState,
                        onLoading: () =>
                        {
                            if (page == 1)
                            {
                                _pbLoading.Visibility = ViewStates.Visible;
                                _rvMain.Visibility = ViewStates.Gone;   
                            }
                            else
                            {
                                _pbLoading.Visibility = ViewStates.Visible;
                            }
                        },
                        onSuccess: state =>
                        {
                            _pbLoading.Visibility = ViewStates.Gone;
                            _rvMain.Visibility = ViewStates.Visible;
                            totalPages = state.Data.Total_Pages;
                            if (page == 1)
                                mainAdapter.SetInitialData(state.Data.Results);
                            else
                                mainAdapter.UpdateData(state.Data.Results);
                        },
                        onError: errorMessage =>
                        {
                            _pbLoading.Visibility = ViewStates.Gone;
                            _rvMain.Visibility = ViewStates.Gone;
                            Toast.MakeText(this, "Data error "+ errorMessage, ToastLength.Long).Show();
                        }
                    );
                }
            };
        }

        private async Task LoadDataMovie(int page = 1)
        {
            try
            {
                var products = await _productViewModel.GetMovies(page);
                totalPages = products.Total_Pages;
                if (products.Results.Count > 0)
                {
                    if (page == 1)
                        mainAdapter.SetInitialData(products.Results);
                    else
                        mainAdapter.UpdateData(products.Results);
                }

                _pbLoading.Visibility = ViewStates.Gone;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: di activity" + ex.Message);
            }
        }

        private async Task LoadDataMovieNews(int page = 1)
        {
            await _productViewModel.GetNewMovies(page);
        }

        private void UpdateUI(State<MovieResponse.Movie> state)
        {
            if (state.IsLoading)
            {
                _pbLoading.Visibility = ViewStates.Visible;
                _rvMain.Visibility = ViewStates.Gone;
                // _errorTextView.Visibility = Android.Views.ViewStates.Gone;
            }
            else if (state.ErrorMessage != null)
            {
                _pbLoading.Visibility = ViewStates.Gone;
                _rvMain.Visibility = ViewStates.Gone;
                Toast.MakeText(this, "Data error", ToastLength.Long).Show();
            }
            else if (state.Data != null)
            {
                _pbLoading.Visibility = ViewStates.Gone;
                _rvMain.Visibility = ViewStates.Visible;
                mainAdapter.UpdateData(state.Data.Results);
            }
        }
        
        
        private void HandleState<T>(
            State<T> state,
            Action onLoading,
            Action<State<T>> onSuccess,
            Action<string> onError
        )
        {
            if (state.IsLoading)
            {
                onLoading();
            }
            else if (state.ErrorMessage != null)
            {
                onError(state.ErrorMessage);
            }
            else if (state.Data != null)
            {
                onSuccess(state);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }

    public class XamarinRecyclerViewOnScrollListener : RecyclerView.OnScrollListener
    {
        public delegate void LoadMoreEventHandler(object sender, EventArgs e);

        private readonly LinearLayoutManager LayoutManager;

        public XamarinRecyclerViewOnScrollListener(LinearLayoutManager layoutManager)
        {
            LayoutManager = layoutManager;
        }

        public event LoadMoreEventHandler LoadMoreEvent;

        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            base.OnScrolled(recyclerView, dx, dy);

            var visibleItemCount = recyclerView.ChildCount;
            var totalItemCount = recyclerView.GetAdapter().ItemCount;
            var pastVisiblesItems = LayoutManager.FindFirstVisibleItemPosition();

            if (visibleItemCount + pastVisiblesItems >= totalItemCount) LoadMoreEvent?.Invoke(this, null);
        }
    }
}