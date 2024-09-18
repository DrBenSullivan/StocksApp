using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using StocksApp.Application.Interfaces;

namespace StocksApp.Application.Services
{
    public class FinnhubService : IFinnhubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            return await GetApiResponse($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token=");
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            return await GetApiResponse($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token=");
        }

        public async Task<Dictionary<string, object>?> GetStocks()
        {
            return await GetApiResponse($"https://finnhub.io/api/v1/stock/symbol?exchange=US");
        }

        private async Task<Dictionary<string, object>?> GetApiResponse(string url)
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                string finnhubSecretKey = _configuration["FinnhubAPIKey"]
                    ?? throw new Exception("Finnhub secret key not found in configuration.");

                var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{url}{finnhubSecretKey}");
                HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage);
                responseMessage.EnsureSuccessStatusCode();

                string responseString = await responseMessage.Content.ReadAsStringAsync();
                Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseString);

                if (responseDictionary == null) throw new InvalidOperationException("No response received from Finnhub API.");
                if (responseDictionary.ContainsKey("error")) throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

                return responseDictionary;
            }
        }
    }
}
