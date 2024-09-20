using Store.WebService.APIs;
using Store.WebService.APIs.Interfaces;
using Store.WebService.Services;
using Store.WebService.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

//Register services
builder.Services.AddScoped<IProductApi, ProductApi>();
builder.Services.AddScoped<IProductWebService, ProductWebService>();
builder.Services.AddScoped<ICategoryApi, CategoryApi>();
builder.Services.AddScoped<ICategoryWebService, CategoryWebService>();
builder.Services.AddScoped<IBannerApi, BannerApi>();
builder.Services.AddScoped<IBannerWebService, BannerWebService>();
builder.Services.AddScoped<INewsApi, NewsApi>();
builder.Services.AddScoped<INewsWebService, NewsWebService>();
builder.Services.AddScoped<IFlashSaleApi, FlashSaleApi>();
builder.Services.AddScoped<IFlashSaleWebService, FlashSaleWebService>();
builder.Services.AddScoped<IStoreApi, StoreApi>();
builder.Services.AddScoped<IStoreWebService, StoreWebService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );
app.MapControllerRoute(
    name: "category",
    pattern: "{controller=ProductCate}/{action=Index}/{categoryId?}",
    defaults: new { controller = "ProductCate" }
);
app.MapControllerRoute(
    name: "detail",
    pattern: "{controller=DetailProduct}/{action=Index}/{productId?}"
);

app.MapControllerRoute(
    name: "ProductSearchResults",
    pattern: "{controller=Common}/{action=ProductSearchResults}/{search?}",
    defaults: new { controller = "Common" }
);

app.Run();
