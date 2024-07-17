using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksAppWithConfiguration.Models;

namespace StocksAppWithConfiguration.Controllers
{
	public class TradeController : Controller
	{
		private readonly TradingOptions _tradingOptions;

		public TradeController(IOptions<TradingOptions> tradingOptions)
		{
			_tradingOptions = tradingOptions.Value;
		}

		[Route("/")]
		[Route("/Trade/Index")]
		public IActionResult Index()
		{
			ViewBag.StockSymbol = _tradingOptions.DefaultStockSymbol;
			return View();
		}
	}
}
