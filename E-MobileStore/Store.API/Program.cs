using Store.ApiService.Services;
using Store.ApiService.Services.Interfaces;
using Store.Infrastructure.Repositories.Interfaces;
using Store.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Store.Infrastructure.Data;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register services
var connectionString = builder.Configuration.GetConnectionString("MobileStore");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAttributeValueRepository, AttributeValueRepository>();
builder.Services.AddScoped<IAttributeValueService, AttributeValueService>();
builder.Services.AddScoped<IFlashSaleRepository, FlashSaleRepository>();
builder.Services.AddScoped<IFlashSaleService, FlashSaleService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IBannerRepository, BannerRepository>();
builder.Services.AddScoped<IBannerService, BannerService>();
builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IStoreService, StoreService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
