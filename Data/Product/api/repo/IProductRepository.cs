using System.Collections.Generic;
using System.Threading.Tasks;

namespace productDemo.Data.Product.api.repo
{
    public interface IProductRepository
    {
        Task<List<model.Product>> GetProducts();
    }
}