using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using StocksApp.Application.Interfaces;
using StocksApp.Presentation.Models.ViewModels;

namespace StocksApp.ViewComponents
{
    [ViewComponent]
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

   //     public async Task<IViewComponentResult> InvokeAsync(string? stockSymbol)
   //     {
			//if (stockSymbol.IsNullOrEmpty()) return View(null);

			//Dictionary<string, object>? companyProfile = await _finnhubService.GetCompanyProfile(stockSymbol)
			//	?? throw new Exception("No response from API from given stock symbol.");

			//var viewModel = new SelectedStockViewModel()
			//{
			//	StockName = companyProfile.name ?? string.Empty,

			//};

   //     }
	}
}
