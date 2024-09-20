using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StocksApp.Application;
using StocksApp.Application.Interfaces;
using StocksApp.Domain.Mapping;
using StocksApp.Domain.Models;
using StocksApp.Persistence;
using StocksApp.Repositories;
using StocksApp.Repositories.Interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services);
});

builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestPropertiesAndHeaders | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
});

Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});
builder.Services.AddHttpClient();
builder.Services.AddScoped<IFinnhubService, FinnhubService>();
builder.Services.AddScoped<IFinnhubRepository, FinnhubRepository>();
builder.Services.AddScoped<IStocksService, StocksService>();
builder.Services.AddScoped<IStocksRepository, StocksRepository>();
builder.Services.Configure<TradingOptions>(
    builder.Configuration.GetSection("TradingOptions")
);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<DomainModelToPresentationModelProfile>();
    config.AddProfile<PresentationModelToDomainModelProfile>();
}, typeof(Program).Assembly);

builder.Services.AddDbContext<StockMarketDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();
app.UseSerilogRequestLogging();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
