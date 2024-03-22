using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using productDemo.Data.Product.api.repo;
using productDemo.Data.Product.implementation.mapper;
using productDemo.Data.Product.implementation.remote.api;
using productDemo.DI;
using Refit;
using Serilog;

namespace productDemo.Data.Product.implementation.repo
{
    public class ProductRepository : IProductRepository
    {
        private readonly IProductService _productService;

        public ProductRepository(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<List<api.model.Product>> GetProducts()
        {
            try
            {
                var productResponses = await _productService.GetProducts();
                return ProductMapper.MapToDomainModel(productResponses.Products);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to get products from the API");
                throw new Exception("Failed to get products from the API", ex);
            }
        }
    }
}