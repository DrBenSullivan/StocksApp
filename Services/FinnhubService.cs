using System.Text.Json;
using System.Text.Json.Nodes;
using StocksAppWithConfiguration.Interfaces;

namespace StocksAppWithConfiguration.Services
{
	public class FinnhubService : IFinnhubService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IConfiguration _configuration;

		public FinnhubService(
			IHttpClientFactory httpClientFactory,
			IConfiguration configuration)
		{
			_httpClientFactory = httpClientFactory;
			_configuration = configuration;
		}

		public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
		{
			using (HttpClient httpClient = _httpClientFactory.CreateClient())
			{
				string? FinnhubSecretKey = _configuration["FinnhubAPIKey"] 
					?? throw new Exception("Finnhub secret key not found in configuration.");

				HttpRequestMessage requestMessage = new HttpRequestMessage()
				{
					RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={FinnhubSecretKey}"),
					Method = HttpMethod.Get
				};

				HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage);

				Stream stream = await responseMessage.Content.ReadAsStreamAsync();
				StreamReader streamReader = new StreamReader(stream);
				string responseString = await streamReader.ReadToEndAsync();

				Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseString);

				return responseDictionary ?? throw new BadHttpRequestException(responseString);
			}
		}

		public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
		{
			using (HttpClient httpClient = _httpClientFactory.CreateClient())
			{
				string? FinnhubSecretKey = _configuration["FinnhubAPIKey"]
					?? throw new Exception("Finnhub secret key not found in configuration.");

				HttpRequestMessage requestMessage = new HttpRequestMessage()
				{
					RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={FinnhubSecretKey}"),
					Method = HttpMethod.Get
				};
				HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage);

				Stream stream = await responseMessage.Content.ReadAsStreamAsync();
				StreamReader streamReader = new StreamReader(stream);
				string responseString = await streamReader.ReadToEndAsync();
				Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseString);

				return responseDictionary ?? throw new BadHttpRequestException(responseString);
			}
		}
	}
}
