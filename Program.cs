using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StocksApp.Application.Interfaces;
using StocksApp.Application.Services;
using StocksApp.Domain.Mapping;
using StocksApp.Domain.Models;
using StocksApp.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews(options =>
{
	options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});
builder.Services.AddHttpClient();
builder.Services.AddScoped<IFinnhubService, FinnhubService>();
builder.Services.AddSingleton<IStocksService, StocksService>();
builder.Services.Configure<TradingOptions>(
	builder.Configuration.GetSection("TradingOptions")
);

builder.Services.AddAutoMapper(config => {
	config.AddProfile<DomainModelToPresentationModelProfile>();
	config.AddProfile<PresentationModelToDomainModelProfile>();
}, typeof(Program).Assembly);

builder.Services.AddDbContext<StockMarketDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
