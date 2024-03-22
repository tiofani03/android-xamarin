using System.Collections.Generic;
using System.Threading.Tasks;
using AndroidX.Lifecycle;
using productDemo.Data.Product.api.repo;

namespace productDemo.Feature.Product.pages.list
{
    public class ProductViewModel : ViewModel
    {
        private readonly IProductRepository _productRepository;

        public ProductViewModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public async Task<List<Data.Product.api.model.Product>> LoadProduct()
        {
            return await _productRepository.GetProducts();
        }
    }
}