using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StocksApp.Application.Interfaces;
using StocksApp.Domain.Models;
using StocksApp.Domain.Validators;
using StocksApp.Persistence;
using StocksApp.Presentation.Models;
using StocksApp.Repositories;
using StocksApp.Repositories.Interfaces;

namespace StocksApp.Application
{
    public class StocksService : IStocksService
    {
        #region private fields
        private readonly IMapper _mapper;
        private readonly IStocksRepository _stocksRepository;
        #endregion

        #region constructors
        public StocksService(IMapper mapper, IStocksRepository stocksRepostiory)
        {
            _stocksRepository = stocksRepostiory;
            _mapper = mapper;
        }
        #endregion

        /// <summary>
        /// Adds a new BuyOrder.
        /// </summary>
        /// <param name="buyOrderRequest">BuyOrder request data to add.</param>
        /// <returns>Returns the BuyOrder data as a SellOrderResponse DTO.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest == null) throw new ArgumentNullException(nameof(buyOrderRequest));

            ModelValidationHelper.Validate(buyOrderRequest);

            BuyOrder buyOrder = _mapper.Map<BuyOrder>(buyOrderRequest);
            buyOrder.BuyOrderID = Guid.NewGuid();

            return _mapper.Map<BuyOrderResponse>(await _stocksRepository.CreateBuyOrder(buyOrder));
        }

        /// <summary>
        /// Adds a new SellOrder.
        /// </summary>
        /// <param name="sellOrderRequest">SellOrder request data to add.</param>
        /// <returns>Returns the SellOrder data as a SellOrderResponse DTO.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null) throw new ArgumentNullException(nameof(sellOrderRequest));

            ModelValidationHelper.Validate(sellOrderRequest);

            SellOrder sellOrder = _mapper.Map<SellOrder>(sellOrderRequest);
            sellOrder.SellOrderID = Guid.NewGuid();

            return _mapper.Map<SellOrderResponse>(await _stocksRepository.CreateSellOrder(sellOrder));
        }

        /// <summary>
        /// Gets a list of all BuyOrders.
        /// </summary>
        /// <returns>Returns the list of BuyOrders as a list of BuyOrderResponse DTOs.</returns>
        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            var buyOrders = await _stocksRepository.GetBuyOrders();

            return buyOrders
                .Select(buyOrder => _mapper.Map<BuyOrderResponse>(buyOrder))
                .ToList();
        }

        /// <summary>
        /// Gets a list of all SellOrders.
        /// </summary>
        /// <returns>Returns the list of SellOrders as a list of SellOrderResponse DTOs.</returns>
        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            var sellOrders = await _stocksRepository.GetSellOrders();

            return sellOrders
                .Select(sellOrder => _mapper.Map<SellOrderResponse>(sellOrder))
                .ToList();
        }
    }
}
