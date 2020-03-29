using Domain.Products;
using Domain.Shared;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Category> _categoryRepository;
        private IGenericRepository<Product> _productRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Category> CategoryRepository
        {
            get { return _categoryRepository ?? (_categoryRepository = new GenericRepository<Category>(_context)); }
        }

        public IGenericRepository<Product> ProductRepository
        {
            get { return _productRepository ?? (_productRepository = new GenericRepository<Product>(_context)); }
        }

        public void Save(string user_id = null, bool deleted = false)
        {
            Audit(user_id, deleted);

            _context.SaveChanges();
        }

        public async Task SaveAsync(string user_id = null, bool deleted = false)
        {
            Audit(user_id, deleted);

            await _context.SaveChangesAsync();
        }

        private void Audit(string user_id = null, bool deleted = false)
        {
            if (string.IsNullOrWhiteSpace(user_id)) return;

            ChangeTracker changes = new ChangeTracker(_context);

            var modifiedEntries = changes.Entries().Where(c => c.Entity is AuditedEntity && c.State == EntityState.Modified || c.State == EntityState.Added);

            foreach (var entry in modifiedEntries)
            {
                var entity = entry.Entity as AuditedEntity;
                var now = DateTime.Now;
                
                if (entry.State == EntityState.Added)
                {
                    entity.CreateDate = now;
                    entity.CreateUserId = user_id;
                }

                entity.UpdateDate = now;
                entity.UpdateUserId = user_id;
                entity.Deleted = deleted;

                if (deleted)
                {
                    entity.DeleteDate = now;
                    entity.DeleteUserId = user_id;
                }
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
