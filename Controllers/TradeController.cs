using Microsoft.AspNetCore.Mvc;

namespace StocksAppWithConfiguration.Controllers
{
	public class TradeController : Controller
	{
		[Route("/")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
