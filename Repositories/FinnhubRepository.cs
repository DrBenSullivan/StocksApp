using System.Text.Json;
using StocksApp.Repositories.Interfaces;

namespace StocksApp.Repositories
{
    public class FinnhubRepository : IFinnhubRepository
    {
        #region private readonly fields
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        #endregion

        #region constructor
        public FinnhubRepository(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        #endregion

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            string response = await GetJsonResponse($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}");
            return JsonSerializer.Deserialize<Dictionary<string, object>>(response);
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            string response = await GetJsonResponse($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}");
            return JsonSerializer.Deserialize<Dictionary<string, object>>(response);
        }

        public async Task<List<Dictionary<string, string>>?> GetStocks()
        {
            string response = await GetJsonResponse($"https://finnhub.io/api/v1/stock/symbol?exchange=US");
            return JsonSerializer.Deserialize<List<Dictionary<string, string>>>(response);
        }

        public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbol)
        {
            string response = await GetJsonResponse($"https://finnhub.io/api/v1/search?q={stockSymbol}");
            return JsonSerializer.Deserialize<Dictionary<string, object>>(response);
        }

        private async Task<string> GetJsonResponse(string url)
        {
            using (HttpClient client = _httpClientFactory.CreateClient())
            {
                string finnhubSecretKey = _configuration["FinnhubAPIKey"]
                    ?? throw new Exception("Finnhub secret key could not be found in this configuration.");

                var request = new HttpRequestMessage(HttpMethod.Get, $"{url}&token={finnhubSecretKey}");
                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
