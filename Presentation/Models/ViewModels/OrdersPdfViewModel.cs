using System.ComponentModel.DataAnnotations;
using StocksApp.Domain.Models;

namespace StocksApp.Presentation.Models.ViewModels
{
	public class OrdersPdfViewModel
	{
		public List<OrderResponse> Orders { get; set; } = new List<OrderResponse>();

		public OrdersPdfViewModel(List<BuyOrderResponse>? buyOrders, List<SellOrderResponse>? sellOrders)
		{
			if (buyOrders != null)
				Orders.AddRange(buyOrders);

			if (sellOrders != null)
				Orders.AddRange(sellOrders);

			Orders = Orders
				.OrderBy(o => o.DateAndTimeOfOrder)
				.ToList();
		}
	}
}

