using StocksApp.Models.DTOs;
using StocksApp.Services;
using StocksApp.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.ConstrainedExecution;
using System;

namespace StocksAppTests
{
	#region StocksService.CreateBuyOrder()
	public class StocksService_CreateBuyOrder_UnitTest
	{
		// When null passed as parameter to CreateBuyOrder, throw ArgumentNullException.
		[Fact]
		public async Task CreateBuyOrder_NullParameter_ThrowsArgumentNullException()
		{
			IStocksService stocksService = new StocksService();
			await Assert.ThrowsAsync<ArgumentNullException>(() => stocksService.CreateBuyOrder(null));
		}


		// When buyOrderRequest passed as parameter with property Quantity = 0 (Specified range 1-100000), throw ArgumentException.
		[Fact]
		public async Task CreateBuyOrder_BuyOrderRequest_QuantityNull_ThrowsArgumentException()
		{
			IStocksService stocksService = new StocksService();
			BuyOrderRequest testBuyOrderRequest = new BuyOrderRequest()
			{
				StockSymbol = "TEST",
				StockName = "Test",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 0,
				Price = 1
			};

			await Assert.ThrowsAsync<ArgumentException>(() => stocksService.CreateBuyOrder(testBuyOrderRequest));
		}


		// When buyOrderRequest passed as parameter with property Quantity = 100001 (Specified range 1-100000), throw ArgumentException.
		[Fact]
		public async Task CreateBuyOrder_BuyOrderRequest_Quantity100001_ThrowsArgumentException()
		{
			IStocksService stocksService = new StocksService();
			BuyOrderRequest testBuyOrderRequest = new BuyOrderRequest()
			{
				StockSymbol = "TEST",
				StockName = "Test",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 100001,
				Price = 1
			};

			await Assert.ThrowsAsync<ArgumentException>(() => stocksService.CreateBuyOrder(testBuyOrderRequest));
		}


		// When buyOrderRequest passed as parameter with property Price = 0 (Specified range 1-10000), throw ArgumentException.
		[Fact]
		public async Task CreateBuyOrder_BuyOrderRequest_Quantity0_ThrowsArgumentException()
		{
			IStocksService stocksService = new StocksService();
			BuyOrderRequest testBuyOrderRequest = new BuyOrderRequest()
			{
				StockSymbol = "TEST",
				StockName = "Test",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 1,
				Price = 0
			};

			await Assert.ThrowsAsync<ArgumentException>(() => stocksService.CreateBuyOrder(testBuyOrderRequest));
		}


		// When buyOrderRequest passed as parameter with property Price = 10001 (Specified range 1-10000), throw ArgumentException.
		[Fact]
		public async Task CreateBuyOrder_BuyOrderRequest_Price10001_ThrowsArgumentException()
		{
			IStocksService stocksService = new StocksService();
			BuyOrderRequest testBuyOrderRequest = new BuyOrderRequest()
			{
				StockSymbol = "TEST",
				StockName = "Test",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 1,
				Price = 10001
			};

			await Assert.ThrowsAsync<ArgumentException>(() => stocksService.CreateBuyOrder(testBuyOrderRequest));
		}


		// When buyOrderRequest passed as a parameter with stockSymbol = null, throw ArgumentException.
		[Fact]
		public async Task CreateBuyOrder_BuyOrderRequest_StockSymbolNull_ThrowsArgumentException()
		{
			IStocksService stocksService = new StocksService();
			BuyOrderRequest testBuyOrderRequest = new BuyOrderRequest()
			{
				StockSymbol = null,
				StockName = "Test",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 1,
				Price = 1
			};

			await Assert.ThrowsAsync<ArgumentException>(() => stocksService.CreateBuyOrder(testBuyOrderRequest));
		}

		// When you supply dateAndTimeOfOrder as "1999-12-31" (YYYY-MM-DD) - (as per the specification, it should be equal or newer date than 2000-01-01), it should throw ArgumentException.
		[Fact]
		public async Task CreateBuyOrder_BuyOrderRequest_DateAndTimeOfOrderYearLessThan2000_ThrowsArgumentException()
		{
			IStocksService stocksService = new StocksService();
			BuyOrderRequest testBuyOrderRequest = new BuyOrderRequest()
			{
				StockSymbol = "TEST",
				StockName = "Test",
				DateAndTimeOfOrder = new DateTime(1999, 12, 31),
				Quantity = 1,
				Price = 1
			};

			await Assert.ThrowsAsync<ArgumentException>(() => stocksService.CreateBuyOrder(testBuyOrderRequest));
		}

		// When a proper buyOrderRequest is passed as a parameter to CreateBuyOrder, returns a buyOrderResponse with a valid GUID and equal properties to the request.
		[Fact]
		public async Task CreateBuyOrder_ProperBuyOrderRequest_ReturnsBuyOrderResponseWithValidGUID()
		{
			IStocksService stocksService = new StocksService();
			BuyOrderRequest testBuyOrderRequest = new BuyOrderRequest()
			{
				StockSymbol = "TEST",
				StockName = "Test",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 1,
				Price = 1
			};

			BuyOrderResponse outputBuyOrderResponse = await stocksService.CreateBuyOrder(testBuyOrderRequest);

			Assert.True(outputBuyOrderResponse.BuyOrderID != Guid.Empty &&
						outputBuyOrderResponse.StockSymbol == testBuyOrderRequest.StockSymbol &&
						outputBuyOrderResponse.StockName == testBuyOrderRequest.StockName &&
						outputBuyOrderResponse.DateAndTimeOfOrder == testBuyOrderRequest.DateAndTimeOfOrder &&
						outputBuyOrderResponse.Quantity == testBuyOrderRequest.Quantity &&
						outputBuyOrderResponse.Price == testBuyOrderRequest.Price &&
						outputBuyOrderResponse.TradeAmount != double.NaN);
		}
	}
	#endregion

	#region StocksService.CreateSellOrder()
	public class StocksService_CreateSellOrder_UnitTest
	{
		// When null passed as parameter to CreateSellOrder, throw ArgumentNullException.
		[Fact]
		public async Task CreateSellOrder_NullParameter_ThrowsArgumentNullException()
		{
			IStocksService stocksService = new StocksService();
			await Assert.ThrowsAsync<ArgumentNullException>(() => stocksService.CreateSellOrder(null));
		}

		// When a sellOrderRequest with Quantity = 0 (specified range 1-100000) passed as paramter to CreateSellOrder, throw ArgumentException.
		[Fact]
		public async Task CreateSellOrder_SellOrderRequest_QuantityNull_ThrowsArgumentException()
		{
			IStocksService stocksService = new StocksService();
			SellOrderRequest testSellOrderRequest = new SellOrderRequest()
			{
				StockSymbol = "TEST",
				StockName = "Test",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 0,
				Price = 1
			};

			await Assert.ThrowsAsync<ArgumentException>(() => stocksService.CreateSellOrder(testSellOrderRequest));
		}

		// When a sellOrderRequest with Quantity = 100001 (specified range 1-100000) passed as parameter to CreateSellOrder, throw ArgumentException.
		[Fact]
		public async Task CreateSellOrder_SellOrderRequest_Quantity100001_ThrowsArgumentException()
		{
			IStocksService stocksService = new StocksService();
			SellOrderRequest testSellOrderRequest = new SellOrderRequest()
			{
				StockSymbol = "TEST",
				StockName = "Test",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 100001,
				Price = 1
			};

			await Assert.ThrowsAsync<ArgumentException>(() => stocksService.CreateSellOrder(testSellOrderRequest));
		}

		// When a sellOrderRequest with Price = 0 (specified range 0-10000) passed as parameter to CreateSellOrder, throw ArgumentException.
		[Fact]
		public async Task CreateSellOrder_SellOrderRequest_Price0_ThrowsArgumentException()
		{
			IStocksService stocksService = new StocksService();
			SellOrderRequest testSellOrderRequest = new SellOrderRequest()
			{
				StockSymbol = "TEST",
				StockName = "Test",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 1,
				Price = 0
			};

			await Assert.ThrowsAsync<ArgumentException>(() => stocksService.CreateSellOrder(testSellOrderRequest));
		}

		// When a sellOrderRequest with Price = 10001 (specified range 0-10000) passed as parameter to CreateSellOrder, throw ArgumentException.
		[Fact]
		public async Task CreateSellOrder_SellOrderRequest_Price10001_ThrowsArgumentException()
		{
			IStocksService stocksService = new StocksService();
			SellOrderRequest testSellOrderRequest = new SellOrderRequest()
			{
				StockSymbol = "TEST",
				StockName = "Test",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 1,
				Price = 10001
			};

			await Assert.ThrowsAsync<ArgumentException>(() => stocksService.CreateSellOrder(testSellOrderRequest));
		}

		// When you supply stock symbol=null (as per the specification, stock symbol can't be null), it should throw ArgumentException
		// When you supply dateAndTimeOfOrder as "1999-12-31" (YYYY-MM-DD) - (as per the specification, it should be equal or newer date than 2000-01-01), it should throw ArgumentException.
		// If you supply all valid values, it should be successful and return an object of SellOrderResponse type with auto-generated SellOrderID(guid).
	}

	#endregion
}