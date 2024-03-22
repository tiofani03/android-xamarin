using System.Threading.Tasks;
using productDemo.Data.Product.implementation.remote.response;
using Refit;

namespace productDemo.Data.Product.implementation.remote.api
{
    public interface IProductService
    {
        [Get("/products")]
        Task<ProductContainerResponse> GetProducts();
    }
}