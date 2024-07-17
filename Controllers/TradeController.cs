using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksAppWithConfiguration.Interfaces;
using StocksAppWithConfiguration.Models;

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
		public IActionResult Index()
		{
			return View();
		}
	}
}
