﻿using StocksApp.Domain.Models;

namespace StocksApp.Application.Interfaces
{
    public interface IFinnhubService
    {
        Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);
        Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol);
        Task<List<Dictionary<string, string>>?> GetStocks();
    }
}
