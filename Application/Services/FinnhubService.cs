using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text.Json;
using StocksApp.Application.Interfaces;
using StocksApp.Domain.Models;

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

        public async Task<List<FinnhubStock>?> GetStocks()
        {
            string apiResponse = await GetApiResponseString($"https://finnhub.io/api/v1/stock/symbol?exchange=US&token=");
            List<FinnhubStock> stocks = JsonSerializer.Deserialize<List<FinnhubStock>>(apiResponse)
                ?? throw new Exception("Finnhub API response could not be deserialised.");

            return stocks;
        }

        private async Task<string> GetApiResponseString(string url)
        { 
            using  (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                string finnhubSecretKey = _configuration["FinnhubAPIKey"]
                    ?? throw new Exception("Finnhub secret key not found in configuration.");

                var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{url}{finnhubSecretKey}");
                HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage);
                responseMessage.EnsureSuccessStatusCode();

                return await responseMessage.Content.ReadAsStringAsync();
            }
        }

        private async Task<Dictionary<string, object>?> GetApiResponse(string url)
        {
            string responseString = await GetApiResponseString(url);
            Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseString);

            if (responseDictionary == null) throw new InvalidOperationException("No response received from Finnhub API.");
            if (responseDictionary.ContainsKey("error")) throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

            return responseDictionary;
        }
    }
}
