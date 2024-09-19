using Microsoft.EntityFrameworkCore;
using StocksApp.Domain.Models;
using StocksApp.Persistence;
using StocksApp.Presentation.Models;
using StocksApp.Repositories.Interfaces;

namespace StocksApp.Repositories
{
    public class StocksRepository : IStocksRepository
    {
        #region private readonly fields
        private readonly StockMarketDbContext _db;
        #endregion

        #region constructor
        public StocksRepository(StockMarketDbContext db)
        {
            _db = db;
        }
        #endregion

        public async Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder)
        {
            try
            {
                await _db.BuyOrders.AddAsync(buyOrder);
                await _db.SaveChangesAsync();
                return buyOrder;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while saving the buy order.", ex);
            }
        }

        public async Task<SellOrder> CreateSellOrder(SellOrder sellOrder)
        {
            try
            {
                await _db.SellOrders.AddAsync(sellOrder);
                await _db.SaveChangesAsync();
                return sellOrder;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while saving the sell order.", ex);
            }
        }

        public async Task<List<BuyOrder>> GetBuyOrders()
        {
            return _db.BuyOrders.Any()
                ? await _db.BuyOrders.ToListAsync()
                : [];
        }

        public async Task<List<SellOrder>> GetSellOrders()
        {
            return _db.SellOrders.Any()
                ? await _db.SellOrders.ToListAsync()
                : [];
        }
    }
}
