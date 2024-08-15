using Microsoft.AspNetCore.Mvc;
using StocksApp.Application.Interfaces;
using StocksApp.Application.Services;
using StocksApp.Domain.Mapping;
using StocksApp.Domain.Models;

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

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
