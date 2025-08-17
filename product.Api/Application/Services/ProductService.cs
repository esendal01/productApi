using product.Api.Application.DTOs;
using product.Api.Domain.Entities;
using product.Api.Domain.Repositories;

namespace product.Api.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var entity = new ProductEntity
            {
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description
            };

            var created = await _repository.AddAsync(entity);

            return new ProductDto
            {
                Id = created.Id,
                Name = created.Name,
                Price = created.Price,
                Description = created.Description
            };
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            return new ProductDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                Description = entity.Description
            };
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(e => new ProductDto
            {
                Id = e.Id,
                Name = e.Name,
                Price = e.Price,
                Description = e.Description
            }).ToList();
        }

        public async Task UpdateAsync(UpdateProductDto dto)
        {
            var entity = await _repository.GetByIdAsync(dto.Id);
            if (entity == null) return;

            entity.Name = dto.Name;
            entity.Price = dto.Price;
            entity.Description = dto.Description;

            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null)
            {
                await _repository.DeleteAsync(entity);
            }
        }
    }
}