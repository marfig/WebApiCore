using Domain.Products;
using Infrastructure.UnitOfWork;
using Services.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Products
{
    public class CategoryService : ICategoryService
    {
        #region Properties
        private IUnitOfWork _unitOfWork;
        #endregion

        #region Settings
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
        #endregion

        #region Methods

        public async Task<List<Category>> GetByValueAsync(string value)
        {
            //return await _unitOfWork.CategoryRepository.GetAsync(m => m.CategoryName == value || m.Description == value, m => m.OrderBy(x => x.CategoryName), x => x.EntidadRelacionadaASerCargada);

            return await _unitOfWork.CategoryRepository.GetAsync(m => !m.Deleted && m.CategoryName == value || m.Description == value, m => m.OrderBy(x => x.CategoryName));
        }

        public List<CategoryDto> GetByValueSomeProperties(string value)
        {
            var CategoryDtoList = _unitOfWork.CategoryRepository
            .Query(x => !x.Deleted && x.CategoryName == value || x.Description == value)
            .Select(x => new CategoryDto
            {
                Name = x.CategoryName
            })
            .ToList();

            return CategoryDtoList;
        }

        #endregion
    }
}
