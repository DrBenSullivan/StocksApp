﻿using Microsoft.AspNetCore.Mvc;

namespace StocksAppWithConfiguration.Controllers
{
	public class HomeController : Controller
	{
		[Route("/")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
