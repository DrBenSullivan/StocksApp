using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
using StocksApp.Application.Interfaces;
using StocksApp.Domain.Models;
using StocksApp.Presentation.Models;
using StocksApp.Presentation.Models.ViewModels;

namespace StocksApp.Controllers
{
    public class TradeController : Controller
    {
		#region private readonly fields
		private readonly TradingOptions _tradingOptions;
        private readonly IFinnhubService _finnhubService;
        private readonly IStocksService _stocksService;
        private readonly IConfiguration _configuration;
		#endregion

		#region constructors
		public TradeController(IOptions<TradingOptions> tradingOptions, IFinnhubService finnhubService, IStocksService stocksService, IConfiguration configuration)
        {
            _tradingOptions = tradingOptions.Value;
            _finnhubService = finnhubService;
            _stocksService = stocksService;
            _configuration = configuration;
        }
		#endregion

		[HttpGet]
        [Route("/")]
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

                string stockSymbol = companyProfile
                    .GetValueOrDefault("ticker")?
                    .ToString()
                    ?? "Unknown";
                string stockName = companyProfile
                    .GetValueOrDefault("name")?
                    .ToString()
                    ?? "Unknown";
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

                ViewBag.FinnhubAPIKey = _configuration["FinnhubAPIKey"];
                return View(stockTradeViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View(null);
            }
        }

		[HttpPost]
        [Route("/BuyOrder")]
        public async Task<IActionResult> BuyOrder(BuyOrderRequest request)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Orders", "Trade", request);
            }    

            request.DateAndTimeOfOrder = DateTime.Now;
            BuyOrderResponse response = await _stocksService.CreateBuyOrder(request);
            return new RedirectToActionResult("Orders", "Trade", new { });
        }

        [HttpPost]
        [Route("/SellOrder")]
        public async Task<IActionResult> SellOrder(SellOrderRequest request)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Orders", "Trade", request);
            }

            request.DateAndTimeOfOrder = DateTime.Now;
            SellOrderResponse response = await _stocksService.CreateSellOrder(request);
            return new RedirectToActionResult("Orders", "Trade", new { });
        }

        [HttpGet]
        [Route("/Orders")]
        public async Task<IActionResult> Orders()
        {
            List<BuyOrderResponse> buyOrders = await _stocksService.GetBuyOrders();
            List<SellOrderResponse> sellOrders = await _stocksService.GetSellOrders();
            var viewModel = new OrdersViewModel() { BuyOrders = buyOrders, SellOrders = sellOrders };
            return View(viewModel);
        }

        [HttpGet]
        [Route("/OrdersPDF")]
        public async Task<IActionResult> OrdersPdf()
        {
            List<BuyOrderResponse> buyOrders = await _stocksService.GetBuyOrders();
			List<SellOrderResponse> sellOrders = await _stocksService.GetSellOrders();
            var viewModel = new OrdersPdfViewModel(buyOrders, sellOrders);
            return new ViewAsPdf("OrdersPdf", viewModel, ViewData)
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins()
                {
                    Top = 20,
                    Right = 20,
                    Bottom = 20,
                    Left = 20
                },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
            };
		}
    }
}