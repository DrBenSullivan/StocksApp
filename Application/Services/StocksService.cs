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
            if (sellOrderRequest == null) throw new ArgumentNullException(nameof(sellOrderRequest));

            ModelValidationHelper.Validate(sellOrderRequest);

            SellOrder sellOrder = _mapper.Map<SellOrder>(sellOrderRequest);
            sellOrder.SellOrderID = Guid.NewGuid();
            _sellOrderList.Add(sellOrder);
            
            return _mapper.Map<SellOrderResponse>(sellOrder);
        }

        public List<BuyOrderResponse> GetBuyOrders()
        {
            if (_buyOrderList.Count == 0) return new List<BuyOrderResponse>();

            return _buyOrderList.Select(buyOrder => _mapper.Map<BuyOrderResponse>(buyOrder)).ToList();
        }

        public List<SellOrderResponse> GetSellOrders()
        {
            if (_sellOrderList.Count == 0) return new List<SellOrderResponse>();

            return _sellOrderList.Select(sellOrder => _mapper.Map<SellOrderResponse>(sellOrder)).ToList();
        }
    }
}
