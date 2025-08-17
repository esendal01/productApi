using System.ComponentModel.DataAnnotations;

namespace product.Api.Application.DTOs
{
    public record ProductCreateDto(
        [Required, StringLength(100)] string Name,
        [Range(0.01, double.MaxValue, ErrorMessage = "Price 0'dan büyük olmalı")] decimal Price,
        [StringLength(500)] string? Description
    );

    public record ProductUpdateDto(
        [Required, StringLength(100)] string Name,
        [Range(0.01, double.MaxValue, ErrorMessage = "Price 0'dan büyük olmalı")] decimal Price,
        [StringLength(500)] string? Description
    );

    public record ProductReadDto(
        int Id,
        string Name,
        decimal Price,
        string? Description,
        DateTime CreatedAt
    );
}
