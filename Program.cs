using StocksAppWithConfiguration.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.Configure<TradingOptions>(
	builder.Configuration.GetSection("TradingOptions")
);

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
