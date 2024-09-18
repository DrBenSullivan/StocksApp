using Microsoft.AspNetCore.Mvc;

namespace StocksApp.Controllers
{
	public class StocksController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
