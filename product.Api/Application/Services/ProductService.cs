using product.Api.Application.DTOs;
using product.Api.Domain.Entities;
using product.Api.Domain.Repositories;

namespace product.Api.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        public ProductService(IProductRepository repo) => _repo = repo;

        public async Task<ProductReadDto> CreateAsync(ProductCreateDto dto)
        {
            var entity = new ProductEntity
            {
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description
            };
            await _repo.AddAsync(entity);
            return new ProductReadDto(entity.Id, entity.Name, entity.Price, entity.Description, entity.CreatedAt);
        }

        public async Task<ProductReadDto?> UpdateAsync(int id, ProductUpdateDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null) return null;

            entity.Name = dto.Name;
            entity.Price = dto.Price;
            entity.Description = dto.Description;
            await _repo.UpdateAsync(entity);

            return new ProductReadDto(entity.Id, entity.Name, entity.Price, entity.Description, entity.CreatedAt);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null) return false;

            await _repo.DeleteAsync(entity);
            return true;
        }

        public async Task<List<ProductReadDto>> GetAllAsync()
        {
            var items = await _repo.GetAllAsync();
            return items.Select(p =>
                new ProductReadDto(p.Id, p.Name, p.Price, p.Description, p.CreatedAt)
            ).ToList();
        }

        public async Task<ProductReadDto?> GetByIdAsync(int id)
        {
            var p = await _repo.GetByIdAsync(id);
            return p is null ? null :
                new ProductReadDto(p.Id, p.Name, p.Price, p.Description, p.CreatedAt);
        }
    }
}
