using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AndroidX.Lifecycle;
using productDemo.Data.Core;
using productDemo.Data.Movie.api;
using productDemo.Data.Movie.impl.remote.model;
using productDemo.Data.Product.api.repo;

namespace productDemo.Feature.Product.pages.list
{
    public class ProductViewModel : ViewModel
    {
        private readonly IProductRepository _productRepository;
        private readonly IMovieRepository _movieRepository;

        public ProductViewModel(IProductRepository productRepository, IMovieRepository movieRepository)
        {
            _productRepository = productRepository;
            _movieRepository = movieRepository;
        }
        
        
        private State<MovieResponse.Movie> _movieState;
        public State<MovieResponse.Movie> MovieState
        {
            get => _movieState;
            set
            {
                _movieState = value;
                OnPropertyChanged();
            }
        }
        
        public async Task GetNewMovies(int page = 2)
        {
            try
            {
                MovieState = State<MovieResponse.Movie>.Loading();
                MovieState = await _movieRepository.GetMoviesNew(page);
            }
            catch (Exception e)
            {
                MovieState = State<MovieResponse.Movie>.Error(e.Message);
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public async Task<List<Data.Product.api.model.Product>> LoadProduct(int page = 1)
        {
            return await _productRepository.GetProducts();
        }
        
        public async Task<MovieResponse.Movie> GetMovies(int page = 2)
        {
            return await _movieRepository.GetMovies(page);
        }
        
        
        
        
    }
}