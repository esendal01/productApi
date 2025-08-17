using Microsoft.EntityFrameworkCore;
using product.Api.Infrastructure.Data;
using product.Api.Infrastructure.Repositories; // en üst using’lere ekle
using product.Api.Domain.Repositories;        // IProductRepository için
using product.Api.Application.Services;
using product.Api.Application.DTOs;
using product.Api.Middlewares; 


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseGlobalExceptionHandling();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();