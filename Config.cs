using Microsoft.AspNetCore.Mvc;
using StocksApp.Application.Interfaces;
using StocksApp.Application;
using StocksApp.Domain.Mapping;
using StocksApp.Domain.Models;
using StocksApp.Persistence;
using StocksApp.Repositories.Interfaces;
using StocksApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace StocksApp
{
    public static class Config
    {
        public static IServiceCollection UseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpLogging(options =>
            {
                options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestPropertiesAndHeaders | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
            });

            Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
            services.AddHttpClient();
            services.AddScoped<IFinnhubService, FinnhubService>();
            services.AddScoped<IFinnhubRepository, FinnhubRepository>();
            services.AddScoped<IStocksService, StocksService>();
            services.AddScoped<IStocksRepository, StocksRepository>();
            services.Configure<TradingOptions>(
                configuration.GetSection("TradingOptions")
            );

            services.AddAutoMapper(config =>
            {
                config.AddProfile<DomainModelToPresentationModelProfile>();
                config.AddProfile<PresentationModelToDomainModelProfile>();
            }, typeof(Program).Assembly);

            services.AddDbContext<StockMarketDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
