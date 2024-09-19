using System.Text.Json;
using StocksApp.Application.Interfaces;
using StocksApp.Domain.Models;
using StocksApp.Repositories.Interfaces;

namespace StocksApp.Application
{
    public class FinnhubService : IFinnhubService
    {
		#region private readonly fields
        private readonly IFinnhubRepository _finnhubRepository;
        #endregion

        #region constructor
        public FinnhubService(IFinnhubRepository finnhubRepository)
        {
            _finnhubRepository = finnhubRepository;
        }
		#endregion

		public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
			return await _finnhubRepository.GetCompanyProfile(stockSymbol);
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            return await _finnhubRepository.GetStockPriceQuote(stockSymbol);
        }

        public async Task<List<Dictionary<string, string>>?> GetStocks()
        {
            return await _finnhubRepository.GetStocks();
        }
    }
}
