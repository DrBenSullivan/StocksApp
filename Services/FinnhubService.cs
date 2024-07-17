using StocksAppWithConfiguration.Interfaces;

namespace StocksAppWithConfiguration.Services
{
	public class FinnhubService : IFinnhubService
	{
		public Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
		{
			throw new NotImplementedException();
		}

		public Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
		{
			throw new NotImplementedException();
		}
	}
}
