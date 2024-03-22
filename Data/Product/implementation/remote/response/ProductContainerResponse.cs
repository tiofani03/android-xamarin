using System.Collections.Generic;

namespace productDemo.Data.Product.implementation.remote.response
{
    public class ProductContainerResponse
    {
        public List<ProductResponse> Products { get; set; }
        public int Total { get; set; }
        public int Skip { get; set; }
        public int Limit { get; set; }
    }
}