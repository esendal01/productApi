using Microsoft.EntityFrameworkCore;
using product.Api.Domain.Entities;

namespace product.Api.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<ProductEntity> Products => Set<ProductEntity>();
    }
}