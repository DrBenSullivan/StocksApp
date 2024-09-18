using Microsoft.AspNetCore.Mvc;
using StocksApp.Application.Interfaces;

namespace StocksApp.Controllers
{
	public class StocksController : Controller
	{
		#region private readonly fields
		private readonly IFinnhubService _finnhubService;
		#endregion

		#region constructor
		public StocksController(IFinnhubService finnhubService)
		{
			_finnhubService = finnhubService;
		}
		#endregion

		public async Task<IActionResult> Index()
		{
			try
			{
				Dictionary<string, object> stocksDict = await _finnhubService.GetStocks()
					?? throw new Exception("Failed to retrieve stocks data from FinnhubAPI.");
				return View(stocksDict);
			}

			catch (Exception ex) 
			{
				Console.WriteLine(ex.Message);
				return View(null);
			}
		}
	}
}
