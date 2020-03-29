using Domain.Products;
using Infrastructure.Repositories;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<Category> CategoryRepository { get; }
        IGenericRepository<Product> ProductRepository { get; }
        void Save(string user_id = null, bool deleted = false);
        Task SaveAsync(string user_id = null, bool deleted = false);
        void Dispose();
    }
}
