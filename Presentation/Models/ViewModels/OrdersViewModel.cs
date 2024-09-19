namespace StocksApp.Presentation.Models.ViewModels
{
    public class OrdersViewModel
    {
        public List<BuyOrderResponse> BuyOrders { get; set; }
        public List<SellOrderResponse> SellOrders { get; set; }
    }
}
