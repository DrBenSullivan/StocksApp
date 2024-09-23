using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace StocksApp.Controllers
{
	public class HomeController : Controller
	{
		[Route("Error")]
		public IActionResult Error()
		{
			IExceptionHandlerFeature? feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
			if (feature != null && feature.Error != null)
			{
				ViewBag.ErrorMessage = feature.Error.Message;
			}
			return View();
		}
	}
}
