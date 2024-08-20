using Microsoft.EntityFrameworkCore;
using StocksApp.Domain.Models;

namespace StocksApp.Persistence
{
	public class StockMarketDbContext : DbContext
	{
		public DbSet<BuyOrder> BuyOrders { get; set; }
		public DbSet<SellOrder> SellOrders { get; set; }
	}
}
