using Domain.Products;
using Infrastructure.Helpers;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services.Products
{
    public class ProductService: IProductService
    {
        #region Properties
        private IUnitOfWork _unitOfWork;
        #endregion

        #region Settings
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
        #endregion

        #region Methods

        public async Task<List<Product>> GetAllAsync()
        {
            return await _unitOfWork.ProductRepository.GetAsync(m => !m.Deleted, m => m.OrderBy(x => x.ProductName));
        }

        public async Task<PaginatedList<Product>> GetPaginatedAsync(int index, int page_size, string name = null, string category = null)
        {
            List<Expression<Func<Product, bool>>> filters = new List<Expression<Func<Product, bool>>>
            {
                c => !c.Deleted
            };

            if (!string.IsNullOrWhiteSpace(name))
            {
                filters.Add(c => c.ProductName.ToUpper().StartsWith(name.ToUpper()));
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                filters.Add(c => c.Category.CategoryName.ToUpper().StartsWith(category.ToUpper()));
            }

            return await _unitOfWork.ProductRepository.GetPaginatedAsync(index, page_size, filters, x => x.OrderByDescending(c => c.Price));
        }

        public async Task<List<Product>> GetAllWithCategoryAsync()
        {
            return await _unitOfWork.ProductRepository.GetAsync(m => !m.Deleted, m => m.OrderBy(x => x.ProductName), x => x.Category);
        }

        public async Task InsertAsync(Product product, string user_id)
        {
            List<Expression<Func<Product, bool>>> filters = new List<Expression<Func<Product, bool>>>();

            filters.Add(c => c.ProductName == "");

            _unitOfWork.ProductRepository.Insert(product);
            await _unitOfWork.SaveAsync(user_id);
        }

        public async Task DeleteProductAsync(Guid product_id, string user_id)
        {
            _unitOfWork.ProductRepository.Delete(product_id);
            await _unitOfWork.SaveAsync(user_id, true);
        }

        #endregion
    }
}
