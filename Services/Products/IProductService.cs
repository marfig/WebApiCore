using Domain.Products;
using Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Products
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<List<Product>> GetAllWithCategoryAsync();
        Task<PaginatedList<Product>> GetPaginatedAsync(int index, int page_size, string name = null, string category = null);
        Task InsertAsync(Product item, string user_id);
        Task DeleteProductAsync(Guid product_id, string user_id);
    }
}