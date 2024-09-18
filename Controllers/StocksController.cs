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

		public async IActionResult Index()
		{
			Dictionary<object, string> apiResults = await _finnhubService.GetStock
			return View();
		}
	}
}
