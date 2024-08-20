using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StocksApp.Application.Interfaces;
using StocksApp.Domain.Models;
using StocksApp.Domain.Validators;
using StocksApp.Persistence;
using StocksApp.Presentation.Models;

namespace StocksApp.Application.Services
{
    public class StocksService : IStocksService
    {
		#region private fields
		private readonly IMapper _mapper;
        private readonly StockMarketDbContext _ordersDb;
		#endregion

		#region constructors
		public StocksService(IMapper mapper, StockMarketDbContext ordersDb)
        {
            _mapper = mapper;
            _ordersDb = ordersDb;
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
            await _ordersDb.BuyOrders.AddAsync(buyOrder);
            await _ordersDb.SaveChangesAsync();

            return _mapper.Map<BuyOrderResponse>(buyOrder);

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
            await _ordersDb.SellOrders.AddAsync(sellOrder);
            await _ordersDb.SaveChangesAsync();
            
            return _mapper.Map<SellOrderResponse>(sellOrder);
        }

		/// <summary>
		/// Gets a list of all BuyOrders.
		/// </summary>
		/// <returns>Returns the list of BuyOrders as a l        ist of BuyOrderResponse DTOs.</returns>
		public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            if (_ordersDb.BuyOrders.Count() == 0) return new List<BuyOrderResponse>();

            return await _ordersDb.BuyOrders
                .Select(buyOrder => _mapper.Map<BuyOrderResponse>(buyOrder))
                .ToListAsync();
        }

        /// <summary>
        /// Gets a list of all SellOrders.
        /// </summary>
        /// <returns>Returns the list of SellOrders as a list of SellOrderResponse DTOs.</returns>
        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            if (_ordersDb.SellOrders.Count() == 0) return new List<SellOrderResponse>();

            return await _ordersDb.SellOrders
                .Select(sellOrder => _mapper.Map<SellOrderResponse>(sellOrder))
                .ToListAsync();
        }
    }
}
