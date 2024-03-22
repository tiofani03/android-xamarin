#nullable enable
using System.Collections.Generic;

namespace productDemo.Data.Product.implementation.remote.response
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? Rating { get; set; }
        public int? Stock { get; set; }
        public string? Brand { get; set; }
        public string? Category { get; set; }
        public string? Thumbnail { get; set; }  
        public List<string>? Images { get; set; }
    }
}