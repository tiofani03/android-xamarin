using System.Collections.Generic;
using System.Linq;
using productDemo.Data.Product.implementation.remote.response;

namespace productDemo.Data.Product.implementation.mapper
{
    public static class ProductMapper
    {
        public static List<api.model.Product> MapToDomainModel(List<ProductResponse> productResponses)
        {
            return productResponses.Select(p => new api.model.Product
            {
                Id = p.Id, 
                Title = p.Title ?? "Unknown",
                Description = p.Description ?? "No description available",
                Price = p.Price ?? 0,
                DiscountPercentage = p.DiscountPercentage ?? 0,
                Rating = p.Rating ?? 0,
                Stock = p.Stock ?? 0,
                Brand = p.Brand ?? "Unknown",
                Category = p.Category ?? "Unknown",
                Thumbnail = p.Thumbnail ?? "https://example.com/default-thumbnail.jpg",
                Images = p.Images?.ToList() ?? new List<string>()
            }).ToList();
        }
    }
}