using Microsoft.EntityFrameworkCore;
using product.Api.Domain.Entities;
using product.Api.Infrastructure.Data;
using product.Api.Domain.Repositories;

namespace product.Api.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;
        public ProductRepository(AppDbContext db) => _db = db;

        public async Task<ProductEntity> AddAsync(ProductEntity entity)
        {
            _db.Products.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(ProductEntity entity)
        {
            _db.Products.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<List<ProductEntity>> GetAllAsync() =>
            await _db.Products.AsNoTracking().OrderByDescending(p => p.Id).ToListAsync();

        public async Task<ProductEntity?> GetByIdAsync(int id) =>
            await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

        public async Task UpdateAsync(ProductEntity entity)
        {
            _db.Products.Update(entity);
            await _db.SaveChangesAsync();
        }
    }
}