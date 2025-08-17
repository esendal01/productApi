using product.Api.Application.DTOs;

namespace product.Api.Application.Services
{
    public interface IProductService
    {
        Task<ProductDto> CreateAsync(CreateProductDto dto);
        Task<ProductDto?> GetByIdAsync(int id);
        Task<List<ProductDto>> GetAllAsync();
        Task UpdateAsync(UpdateProductDto dto);
        Task DeleteAsync(int id);
    }
}