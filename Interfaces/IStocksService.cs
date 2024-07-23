using StocksApp.Models.DTOs;

namespace StocksApp.Interfaces
{
	public interface IStocksService
	{
		Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest);

		Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest);

		Task<List<BuyOrderResponse>> GetBuyOrders();

		Task<List<SellOrderResponse>> GetSellOrders();
	}
}
