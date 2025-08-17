using product.Api.Application.DTOs;

namespace product.Api.Application.Services
{
    public interface IProductService
    {
        Task<ProductReadDto>  CreateAsync(ProductCreateDto dto);
        Task<ProductReadDto?> UpdateAsync(int id, ProductUpdateDto dto); // 2 parametre
        Task<List<ProductReadDto>> GetAllAsync();
        Task<ProductReadDto?> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id); // bool döner
    }
}
