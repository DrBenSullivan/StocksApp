﻿namespace StocksApp.Repositories.Interfaces
{
	public interface IFinnhubRepository
	{
		public Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);
		public Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol);
		public Task<List<Dictionary<string, string>>?> GetStocks();
		public Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch);
	}
}
