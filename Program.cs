using StocksAppWithConfiguration.Interfaces;
using StocksAppWithConfiguration.Models;
using StocksAppWithConfiguration.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IFinnhubService, FinnhubService>();
builder.Services.Configure<TradingOptions>(
	builder.Configuration.GetSection("TradingOptions")
);

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
