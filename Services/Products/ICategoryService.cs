using Domain.Products;
using Services.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Products
{
    public interface ICategoryService
    {
        Task<List<Category>> GetByValueAsync(string value);
        List<CategoryDto> GetByValueSomeProperties(string value);
    }
}
