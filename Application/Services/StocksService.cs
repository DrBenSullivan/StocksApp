using AutoMapper;
using StocksApp.Application.Interfaces;
using StocksApp.Domain.Models;
using StocksApp.Domain.Validators;
using StocksApp.Presentation.Models;

namespace StocksApp.Application.Services
{
    public class StocksService : IStocksService
    {
        private readonly IMapper _mapper;
        private readonly List<BuyOrder> _buyOrderList;
        private readonly List<SellOrder> _sellOrderList;

        public StocksService() { }

        public StocksService(IMapper mapper)
        {
            _mapper = mapper;
            _buyOrderList = new List<BuyOrder>();
            _sellOrderList = new List<SellOrder>();
        }

        public BuyOrderResponse CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest == null) throw new ArgumentNullException(nameof(buyOrderRequest));

            ModelValidationHelper.Validate(buyOrderRequest);

            BuyOrder buyOrder = _mapper.Map<BuyOrder>(buyOrderRequest);
            buyOrder.BuyOrderID = Guid.NewGuid();
            _buyOrderList.Add(buyOrder);

            return _mapper.Map<BuyOrderResponse>(buyOrder);

        }

        public SellOrderResponse CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            throw new NotImplementedException();
        }

        public List<BuyOrderResponse> GetBuyOrders()
        {
            throw new NotImplementedException();
        }

        public List<SellOrderResponse> GetSellOrders()
        {
            throw new NotImplementedException();
        }
    }
}
