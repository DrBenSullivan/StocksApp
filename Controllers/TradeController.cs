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

		public TradeController(IOptions<TradingOptions> tradingOptions, IFinnhubService finnhubService)
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
					?? throw new Exception("Default Stock Symbol not found in configuration.");

				Dictionary<string, object> stockQuote = await _finnhubService.GetStockPriceQuote(defaultStockSymbol)
					?? throw new Exception("Failed to retrieve stockQuote from finnhubService.");

				Dictionary<string, object> companyProfile = await _finnhubService.GetCompanyProfile(defaultStockSymbol)
					?? throw new Exception("Failed to retrieve companyProfile from finnhubService.");

				string stockSymbol = companyProfile.GetValueOrDefault("ticker")?.ToString() ?? "Unknown";
				string stockName = companyProfile.GetValueOrDefault("name")?.ToString() ?? "Unknown";
				int quantity = companyProfile.TryGetValue("shareOutstanding", out var quantityValue)
					? (int)Convert.ToDouble(quantityValue.ToString())
					: 0;
				double price = stockQuote.TryGetValue("c", out var priceValue) 
					? Convert.ToDouble(priceValue.ToString()) 
					: 0;

				var stockTradeViewModel = new StockTradeViewModel()
				{
					StockSymbol = stockSymbol,
					StockName = stockName,
					Price = price,
					Quantity = quantity
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
