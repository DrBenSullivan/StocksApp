using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksAppWithConfiguration.Interfaces;
using StocksAppWithConfiguration.Models;
using StocksAppWithConfiguration.Models.ViewModels;

namespace StocksAppWithConfiguration.Controllers
{
	public class TradeController : Controller
	{
		private readonly TradingOptions _tradingOptions;
		private readonly IFinnhubService _finnhubService;

		public TradeController(
			IOptions<TradingOptions> tradingOptions,
			IFinnhubService finnhubService)
		{
			_tradingOptions = tradingOptions.Value;
			_finnhubService = finnhubService;
		}

		[Route("/")]
		[Route("/Trade/Index")]
		public async Task<IActionResult> Index()
		{
			try
			{
				string defaultStockSymbol = _tradingOptions.DefaultStockSymbol
					?? throw new Exception("Default Stock Code not found in configuration.");

				Dictionary<string, object>? apiStockPriceQuoteResponse = await _finnhubService.GetStockPriceQuote(defaultStockSymbol)
					?? throw new BadHttpRequestException("No valid response received from Finnhub external API.");

				Dictionary<string, object>? apiCompanyProfileResponse = await _finnhubService.GetCompanyProfile(defaultStockSymbol)
					?? throw new BadHttpRequestException("No valid response received from Finnhub external API.");

				var stockTradeViewModel = new StockTradeViewModel()
				{
					StockSymbol = apiCompanyProfileResponse.GetValueOrDefault("ticker").ToString(),
					StockName = apiCompanyProfileResponse.GetValueOrDefault("name").ToString(),
					Price = double.Parse(apiStockPriceQuoteResponse.GetValueOrDefault("c").ToString()),
					Quantity = (int)double.Parse(apiCompanyProfileResponse.GetValueOrDefault("marketCapitalization").ToString())
				};
				return View(stockTradeViewModel);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return View(null);
			}
		}
	}
}
