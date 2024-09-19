using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StocksApp.Application.Interfaces;
using StocksApp.Presentation.Models.ViewModels;

namespace StocksApp.ViewComponents
{
    [ViewComponent(Name = "SelectedStock")]
    public class SelectedStockViewComponent : ViewComponent
    {
        #region private readonly fields
        private readonly IFinnhubService _finnhubService;
        #endregion

        #region constructor
        public SelectedStockViewComponent(IFinnhubService finnhubService)
        {
            _finnhubService = finnhubService;
        }
        #endregion

        public async Task<IViewComponentResult> InvokeAsync(string? stockSymbol)
        {
            if (stockSymbol.IsNullOrEmpty()) return Content("No Stock Symbol provided");

            try
            {
                var companyProfile = await _finnhubService.GetCompanyProfile(stockSymbol);
                if (companyProfile == null)
                {
                    return Content("No response from Finnhub API Profile for given stock symbol.");
                }


                var stockPriceQuote = await _finnhubService.GetStockPriceQuote(stockSymbol);
                if (stockPriceQuote == null)
                {
                    return Content("No response from Finnhub API Quote for given stock symbol.");
                }

                var viewModel = new SelectedStockViewModel()
                {
                    StockName = companyProfile.ContainsKey("name")
                        ? companyProfile["name"].ToString()
                        : "ERROR",

                    StockSymbol = companyProfile.ContainsKey("ticker")
                        ? companyProfile["ticker"].ToString()
                        : "ERR",

                    LogoUrl = companyProfile.ContainsKey("logo")
                        ? companyProfile["logo"].ToString()
                        : string.Empty,

                    Sector = companyProfile.ContainsKey("finnhubIndustry")
                        ? companyProfile["finnhubIndustry"].ToString()
                        : string.Empty,

                    Exchange = companyProfile.ContainsKey("exchange")
                        ? companyProfile["exchange"].ToString()
                        : string.Empty,

                    Price = stockPriceQuote.ContainsKey("c") && double.TryParse(stockPriceQuote["c"].ToString(), out var parsedPrice)
                        ? Math.Truncate(parsedPrice * 100) / 100
                        : 0
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }
    }
}
