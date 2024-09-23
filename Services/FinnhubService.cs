using StocksApp.Application.Interfaces;
using StocksApp.Exceptions;
using StocksApp.Repositories.Interfaces;

namespace StocksApp.Application
{
	public class FinnhubService : IFinnhubService
    {
        #region private readonly fields
        private readonly IFinnhubRepository _finnhubRepository;
        private readonly ILogger<FinnhubService> _logger;
        #endregion

        #region constructor
        public FinnhubService(IFinnhubRepository finnhubRepository, ILogger<FinnhubService> logger)
        {
            _finnhubRepository = finnhubRepository;
            _logger = logger;
        }
        #endregion

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            _logger.LogInformation($"Executing FinnhubService.GetCompanyProfile({stockSymbol})");
            return await _finnhubRepository.GetCompanyProfile(stockSymbol) 
                ?? throw new FinnhubApiException($"No valid Company Profile from FinnhubApi for stockSymbol {stockSymbol}.");
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            _logger.LogInformation($"Executing FinnhubService.GetStockPriceQuote({stockSymbol})");
            return await _finnhubRepository.GetStockPriceQuote(stockSymbol) 
                ?? throw new FinnhubApiException($"No valid Stock Price Quote from FinnhubApi for stockSymbol {stockSymbol}.");
        }

        public async Task<List<Dictionary<string, string>>?> GetStocks()
        {
            _logger.LogInformation("Executing FinnhubService.GetStocks()");
            return await _finnhubRepository.GetStocks() 
                ?? throw new FinnhubApiException($"No valid Stocks from FinnhubApi.");
        }
    }
}
