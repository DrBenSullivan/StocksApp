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
		private readonly IStocksService _stocksService;

		public StocksService_CreateBuyOrder_UnitTest()
		{
			_stocksService = new StocksService();
		}

		// When null passed as parameter to CreateBuyOrder, throw ArgumentNullException.
		[Fact]
		public async Task CreateBuyOrder_NullParameter_ThrowsArgumentNullException()
		{
			await Assert.ThrowsAsync<ArgumentNullException>(() => _stocksService.CreateBuyOrder(null));
		}


		// When buyOrderRequest passed as parameter with property Quantity = 0 (Specified range 1-100000), throw ArgumentException.
		[Fact]
		public async Task CreateBuyOrder_BuyOrderRequest_QuantityNull_ThrowsArgumentException()
		{
			BuyOrderRequest testBuyOrderRequest = new BuyOrderRequest()
			{
				StockSymbol = "TEST",
				StockName = "Test",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 0,
				Price = 1
			};

			await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateBuyOrder(testBuyOrderRequest));
		}


		// When buyOrderRequest passed as parameter with property Quantity = 100001 (Specified range 1-100000), throw ArgumentException.
		[Fact]
		public async Task CreateBuyOrder_BuyOrderRequest_Quantity100001_ThrowsArgumentException()
		{
			BuyOrderRequest testBuyOrderRequest = new BuyOrderRequest()
			{
				StockSymbol = "TEST",
				StockName = "Test",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 100001,
				Price = 1
			};

			await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateBuyOrder(testBuyOrderRequest));
		}


		// When buyOrderRequest passed as parameter with property Price = 0 (Specified range 1-10000), throw ArgumentException.
		[Fact]
		public async Task CreateBuyOrder_BuyOrderRequest_Quantity0_ThrowsArgumentException()
		{
			BuyOrderRequest testBuyOrderRequest = new BuyOrderRequest()
			{
				StockSymbol = "TEST",
				StockName = "Test",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 1,
				Price = 0
			};

			await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateBuyOrder(testBuyOrderRequest));
		}


		// When buyOrderRequest passed as parameter with property Price = 10001 (Specified range 1-10000), throw ArgumentException.
		[Fact]
		public async Task CreateBuyOrder_BuyOrderRequest_Price10001_ThrowsArgumentException()
		{
			BuyOrderRequest testBuyOrderRequest = new BuyOrderRequest()
			{
				StockSymbol = "TEST",
				StockName = "Test",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 1,
				Price = 10001
			};

			await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateBuyOrder(testBuyOrderRequest));
		}


		// When buyOrderRequest passed as a parameter with stockSymbol = null, throw ArgumentException.
		[Fact]
		public async Task CreateBuyOrder_BuyOrderRequest_StockSymbolNull_ThrowsArgumentException()
		{
			BuyOrderRequest testBuyOrderRequest = new BuyOrderRequest()
			{
				StockSymbol = null,
				StockName = "Test",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 1,
				Price = 1
			};

			await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateBuyOrder(testBuyOrderRequest));
		}

		// When you supply dateAndTimeOfOrder as "1999-12-31" (YYYY-MM-DD) - (as per the specification, it should be equal or newer date than 2000-01-01), it should throw ArgumentException.
		[Fact]
		public async Task CreateBuyOrder_BuyOrderRequest_DateAndTimeOfOrderBefore2000_ThrowsArgumentException()
		{
			BuyOrderRequest testBuyOrderRequest = new BuyOrderRequest()
			{
				StockSymbol = "TEST",
				StockName = "Test",
				DateAndTimeOfOrder = new DateTime(1999, 12, 31),
				Quantity = 1,
				Price = 1
			};

			await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateBuyOrder(testBuyOrderRequest));
		}

		// When a proper buyOrderRequest is passed as a parameter to CreateBuyOrder, returns a buyOrderResponse with a valid GUID and equal properties to the request.
		[Fact]
		public async Task CreateBuyOrder_ProperBuyOrderRequest_ReturnsValidBuyOrderResponse()
		{
			BuyOrderRequest testBuyOrderRequest = new BuyOrderRequest()
			{
				StockSymbol = "TEST",
				StockName = "Test",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 1,
				Price = 1
			};

			BuyOrderResponse outputBuyOrderResponse = await _stocksService.CreateBuyOrder(testBuyOrderRequest);

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
		private readonly IStocksService _stocksService;

		public StocksService_CreateSellOrder_UnitTest()
		{
			_stocksService = new StocksService();
		}

		// When null passed as parameter to CreateSellOrder, throw ArgumentNullException.
		[Fact]
		public async Task CreateSellOrder_NullParameter_ThrowsArgumentNullException()
		{
			await Assert.ThrowsAsync<ArgumentNullException>(() => _stocksService.CreateSellOrder(null));
		}

		// When a sellOrderRequest with Quantity = 0 (specified range 1-100000) passed as paramter to CreateSellOrder, throw ArgumentException.
		[Fact]
		public async Task CreateSellOrder_SellOrderRequest_QuantityNull_ThrowsArgumentException()
		{
			SellOrderRequest testSellOrderRequest = new SellOrderRequest()
			{
				StockSymbol = "TEST",
				StockName = "Test",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 0,
				Price = 1
			};

			await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateSellOrder(testSellOrderRequest));
		}

		// When a sellOrderRequest with Quantity = 100001 (specified range 1-100000) passed as parameter to CreateSellOrder, throw ArgumentException.
		[Fact]
		public async Task CreateSellOrder_SellOrderRequest_Quantity100001_ThrowsArgumentException()
		{
			SellOrderRequest testSellOrderRequest = new SellOrderRequest()
			{
				StockSymbol = "TEST",
				StockName = "Test",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 100001,
				Price = 1
			};

			await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateSellOrder(testSellOrderRequest));
		}

		// When a sellOrderRequest with Price = 0 (specified range 0-10000) passed as parameter to CreateSellOrder, throw ArgumentException.
		[Fact]
		public async Task CreateSellOrder_SellOrderRequest_Price0_ThrowsArgumentException()
		{
			SellOrderRequest testSellOrderRequest = new SellOrderRequest()
			{
				StockSymbol = "TEST",
				StockName = "Test",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 1,
				Price = 0
			};

			await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateSellOrder(testSellOrderRequest));
		}

		// When a sellOrderRequest with Price = 10001 (specified range 0-10000) passed as parameter to CreateSellOrder, throw ArgumentException.
		[Fact]
		public async Task CreateSellOrder_SellOrderRequest_Price10001_ThrowsArgumentException()
		{
			SellOrderRequest testSellOrderRequest = new SellOrderRequest()
			{
				StockSymbol = "TEST",
				StockName = "Test",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 1,
				Price = 10001
			};

			await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateSellOrder(testSellOrderRequest));
		}

		// When a sellOrderRequest with StockSymbol = null passed as parameter to CreateSellOrder, throw ArgumentException.
		[Fact]
		public async Task CreateSellOrder_SellOrderRequest_StockSymbolNull_ThrowsArgumentException()
		{
			SellOrderRequest testSellOrderRequest = new SellOrderRequest()
			{
				StockSymbol = null,
				StockName = "Test",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 1,
				Price = 1
			};

			await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateSellOrder(testSellOrderRequest));
		}

		// When a sellOrderRequest with a DateAndTimeOfOrder is before the year 2000, throw ArgumentException.
		[Fact]
		public async Task CreateSellOrder_SellOrderRequest_DateAndTimeOfOrderBeforeYear2000_ThrowsArgumentException()
		{
			SellOrderRequest testSellOrderRequest = new SellOrderRequest()
			{
				StockSymbol = "TEST",
				StockName = "Test",
				DateAndTimeOfOrder = new DateTime(1999,12,31),
				Quantity = 1,
				Price = 1
			};

			await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateSellOrder(testSellOrderRequest));
		}

		// When a proper sellOrderRequest is passed as a parameter to CreateBuyOrder, returns a sellOrderResponse with a valid GUID and equal properties to the request.
		[Fact]
		public async Task CreateSellOrder_ProperSellOrderRequest_ReturnsValidSellOrderResponse()
		{
			SellOrderRequest testSellOrderRequest = new SellOrderRequest()
			{
				StockSymbol = "TEST",
				StockName = "Test",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 1,
				Price = 1
			};

			SellOrderResponse outputSellOrderResponse = await _stocksService.CreateSellOrder(testSellOrderRequest);

			Assert.True(outputSellOrderResponse.SellOrderID != Guid.Empty &&
						outputSellOrderResponse.StockSymbol == testSellOrderRequest.StockSymbol &&
						outputSellOrderResponse.StockName == testSellOrderRequest.StockName &&
						outputSellOrderResponse.DateAndTimeOfOrder == testSellOrderRequest.DateAndTimeOfOrder &&
						outputSellOrderResponse.Quantity == testSellOrderRequest.Quantity &&
						outputSellOrderResponse.Price == testSellOrderRequest.Price &&
						outputSellOrderResponse.TradeAmount != double.NaN);
		}
	}
	#endregion

	#region StocksService.GetBuyOrders()
	public class StocksService_GetBuyOrders_UnitTest
	{
		private readonly IStocksService _stocksService;

		public StocksService_GetBuyOrders_UnitTest()
		{
			_stocksService = new StocksService();
		}
		
		// By default, GetBuyOrders should return an empty list of BuyOrders.
		[Fact]
		public async Task GetBuyOrders_ReturnsEmptyListAsDefault()
		{
			List<BuyOrderResponse> outputBuyOrderResponseList = await _stocksService.GetBuyOrders();
			Assert.Empty(outputBuyOrderResponseList);
		}

		// After adding buy orders using the CreateBuyOrder() method, GetBuyOrders should return a list containing the same buy orders.
		[Fact]
		public async Task GetBuyOrders_ReturnsCorrectListOfBuyOrdersAfterAddingThem()
		{
			BuyOrderRequest testBuyOrderRequest_1 = new BuyOrderRequest()
			{
				StockSymbol = "TEST1",
				StockName = "Test1",
				DateAndTimeOfOrder = new DateTime(2001,1,1),
				Quantity = 1,
				Price = 1
			};

			BuyOrderRequest testBuyOrderRequest_2 = new BuyOrderRequest()
			{
				StockSymbol = "TEST2",
				StockName = "Test2",
				DateAndTimeOfOrder = new DateTime(2002, 2, 2),
				Quantity = 1,
				Price = 1
			};

			BuyOrderRequest testBuyOrderRequest_3 = new BuyOrderRequest()
			{
				StockSymbol = "TEST3",
				StockName = "Test3",
				DateAndTimeOfOrder = new DateTime(2003, 3, 3),
				Quantity = 1,
				Price = 1
			};

			List<BuyOrderRequest> testBuyOrderRequestList = new List<BuyOrderRequest>()
			{
				testBuyOrderRequest_1, testBuyOrderRequest_2, testBuyOrderRequest_3
			};

			List<BuyOrderResponse> outputBuyOrderResponseList = new List<BuyOrderResponse>();

			foreach (BuyOrderRequest request in testBuyOrderRequestList)
			{
				BuyOrderResponse response = await _stocksService.CreateBuyOrder(request);
				outputBuyOrderResponseList.Add(response);
			}

			List<BuyOrderResponse> buyOrderResponseListFromGet = await _stocksService.GetBuyOrders();

			foreach (BuyOrderResponse buyOrderFromGet in buyOrderResponseListFromGet)
			{
				Assert.Contains(buyOrderFromGet, outputBuyOrderResponseList);
			}
		}
	}
	#endregion
}