using StocksApp.Application.Interfaces;
using StocksApp.Presentation.Models;
using Moq;
using FluentAssertions;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using StocksApp.Domain.Mapping;
using StocksApp.Repositories.Interfaces;
using StocksApp.Application;
using AutoFixture;
using StocksApp.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace StocksAppTests
{
    #region StocksService.CreateBuyOrder()
    public class StocksService_CreateBuyOrder_UnitTest
	{
		private readonly Mock<IStocksRepository> _mockStocksRepository;
		private readonly IStocksRepository _stocksRepository;
		private readonly IStocksService _stocksService;
		private readonly IMapper _mapper;	
		private readonly IFixture _fixture;

		public StocksService_CreateBuyOrder_UnitTest()
		{
			var serviceCollection = new ServiceCollection();
			serviceCollection.AddAutoMapper(typeof(DomainModelToPresentationModelProfile));
			serviceCollection.AddAutoMapper(typeof(PresentationModelToDomainModelProfile));
			var serviceProvider = serviceCollection.BuildServiceProvider();
			_mapper = serviceProvider.GetRequiredService<IMapper>();

			_mockStocksRepository = new Mock<IStocksRepository>();
			_stocksRepository = _mockStocksRepository.Object;
			_stocksService = new StocksService(_mapper, _stocksRepository);
			_fixture = new Fixture();
		}

		// When null passed as parameter to CreateBuyOrder, throw ArgumentNullException.
		[Fact]
		public void CreateBuyOrder_NullParameter_ThrowsArgumentNullException()
		{
			Func<Task> action = async () => await _stocksService.CreateBuyOrder(null);
			action.Should().ThrowAsync<ArgumentNullException>();
		}


		// When buyOrderRequest passed as parameter with property Quantity = 0 (Specified range 1-100000), throw ArgumentException.
		[Fact]
		public void CreateBuyOrder_BuyOrderRequest_QuantityNull_ThrowsArgumentException()
		{
			var testBuyOrderRequest = _fixture.Build<BuyOrderRequest>()
				.With(r => r.Quantity, 0)
				.Create();

			Func<Task> action = async () => await _stocksService.CreateBuyOrder(testBuyOrderRequest);
			action.Should().ThrowAsync<ArgumentException>();
		}


		// When buyOrderRequest passed as parameter with property Quantity = 100001 (Specified range 1-100000), throw ArgumentException.
		[Fact]
		public void CreateBuyOrder_BuyOrderRequest_Quantity100001_ThrowsArgumentException()
		{
			var testBuyOrderRequest = _fixture.Build<BuyOrderRequest>()
				.With(r => r.Quantity, 100001)
				.Create();

			Func<Task> action = async () => await _stocksService.CreateBuyOrder(testBuyOrderRequest);
			action.Should().ThrowAsync<ArgumentException>();
		}


		// When buyOrderRequest passed as parameter with property Price = 0 (Specified range 1-10000), throw ArgumentException.
		[Fact]
		public void CreateBuyOrder_BuyOrderRequest_Quantity0_ThrowsArgumentException()
		{
			var testBuyOrderRequest = _fixture.Build<BuyOrderRequest>()
				.With(r => r.Price, 0)
				.Create();

			Func<Task> action = async () => await _stocksService.CreateBuyOrder(testBuyOrderRequest);
			action.Should().ThrowAsync<ArgumentException>();
		}


		// When buyOrderRequest passed as parameter with property Price = 10001 (Specified range 1-10000), throw ArgumentException.
		[Fact]
		public void CreateBuyOrder_BuyOrderRequest_Price10001_ThrowsArgumentException()
		{
			var testBuyOrderRequest = _fixture.Build<BuyOrderRequest>()
				.With(r => r.Price, 10001)
				.Create();

			Func<Task> action = async () => await _stocksService.CreateBuyOrder(testBuyOrderRequest);
			action.Should().ThrowAsync<ArgumentException>();
		}


		// When buyOrderRequest passed as a parameter with stockSymbol = null, throw ArgumentException.
		[Fact]
		public void CreateBuyOrder_BuyOrderRequest_StockSymbolNull_ThrowsArgumentException()
		{
			var testBuyOrderRequest = _fixture.Build<BuyOrderRequest>()
				.With(r => r.StockSymbol, null as string)
				.Create();

			Func<Task> action = async () => await _stocksService.CreateBuyOrder(testBuyOrderRequest);
			action.Should().ThrowAsync<ArgumentException>();
		}

		// When buyOrderRequest passed as a parameter with a DateAndTimeOfOrder before 01-01-2000, throw ArgumentException.
		[Fact]
		public void CreateBuyOrder_BuyOrderRequest_DateAndTimeOfOrderBefore2000_ThrowsArgumentException()
		{
			var testBuyOrderRequest = _fixture.Build<BuyOrderRequest>()
				.With(r => r.DateAndTimeOfOrder, DateTime.Parse("31-12-1999"))
				.Create();

			Func<Task> action = async () => await _stocksService.CreateBuyOrder(testBuyOrderRequest);
			action.Should().ThrowAsync<ArgumentException>();
		}

		// When buyOrderRequest passed as a parameter with valid properties, returns a buyOrderResponse with a valid GUID and equal properties to the request.
		[Fact]
		public async Task CreateBuyOrder_ProperBuyOrderRequest_ReturnsValidBuyOrderResponseAsync()
		{
			var testBuyOrderRequest = _fixture.Build<BuyOrderRequest>()
				.With(r => r.StockName, "Test")
				.With(r => r.StockSymbol, "TEST")
				.With(r => r.DateAndTimeOfOrder, DateTime.Now)
				.With(r => r.Quantity, 1)
				.With(r => r.Price, 1)
				.Create();

			var expectedBuyOrder = _mapper.Map<BuyOrder>(testBuyOrderRequest);
			var expectedBuyOrderResponse = _mapper.Map<BuyOrderResponse>(expectedBuyOrder);

			_mockStocksRepository.Setup
				(p => p.CreateBuyOrder(It.IsAny<BuyOrder>()))
				.ReturnsAsync(expectedBuyOrder);

            BuyOrderResponse outputBuyOrderResponse = await _stocksService.CreateBuyOrder(testBuyOrderRequest);

			outputBuyOrderResponse.Should().Be(expectedBuyOrderResponse);
		}
	}
	#endregion
}
//	#region StocksService.CreateSellOrder()
//	public class StocksService_CreateSellOrder_UnitTest
//	{
//		private readonly IMapper _mapper;
//		private readonly IStocksService _stocksService;

//		public StocksService_CreateSellOrder_UnitTest()
//		{
//			var serviceCollection = new ServiceCollection();
//			serviceCollection.AddAutoMapper(typeof(DomainModelToPresentationModelProfile));
//			serviceCollection.AddAutoMapper(typeof(PresentationModelToDomainModelProfile));
//			var serviceProvider = serviceCollection.BuildServiceProvider();
//			_mapper = serviceProvider.GetRequiredService<IMapper>();

//			_stocksService = new StocksService(_mapper);
//		}

//		// When null passed as parameter to CreateSellOrder, throw ArgumentNullException.
//		[Fact]
//		public void CreateSellOrder_NullParameter_ThrowsArgumentNullException()
//		{
//			Assert.Throws<ArgumentNullException>(() => _stocksService.CreateSellOrder(null));
//		}

//		// When a sellOrderRequest with Quantity = 0 (specified range 1-100000) passed as paramter to CreateSellOrder, throw ArgumentException.
//		[Fact]
//		public void CreateSellOrder_SellOrderRequest_QuantityNull_ThrowsArgumentException()
//		{
//			SellOrderRequest testSellOrderRequest = new SellOrderRequest()
//			{
//				StockSymbol = "TEST",
//				StockName = "Test",
//				DateAndTimeOfOrder = DateTime.Now,
//				Quantity = 0,
//				Price = 1
//			};

//			Assert.Throws<ArgumentException>(() => _stocksService.CreateSellOrder(testSellOrderRequest));
//		}

//		// When a sellOrderRequest with Quantity = 100001 (specified range 1-100000) passed as parameter to CreateSellOrder, throw ArgumentException.
//		[Fact]
//		public void CreateSellOrder_SellOrderRequest_Quantity100001_ThrowsArgumentException()
//		{
//			SellOrderRequest testSellOrderRequest = new SellOrderRequest()
//			{
//				StockSymbol = "TEST",
//				StockName = "Test",
//				DateAndTimeOfOrder = DateTime.Now,
//				Quantity = 100001,
//				Price = 1
//			};

//			Assert.Throws<ArgumentException>(() => _stocksService.CreateSellOrder(testSellOrderRequest));
//		}

//		// When a sellOrderRequest with Price = 0 (specified range 0-10000) passed as parameter to CreateSellOrder, throw ArgumentException.
//		[Fact]
//		public void CreateSellOrder_SellOrderRequest_Price0_ThrowsArgumentException()
//		{
//			SellOrderRequest testSellOrderRequest = new SellOrderRequest()
//			{
//				StockSymbol = "TEST",
//				StockName = "Test",
//				DateAndTimeOfOrder = DateTime.Now,
//				Quantity = 1,
//				Price = 0
//			};

//			Assert.Throws<ArgumentException>(() => _stocksService.CreateSellOrder(testSellOrderRequest));
//		}

//		// When a sellOrderRequest with Price = 10001 (specified range 0-10000) passed as parameter to CreateSellOrder, throw ArgumentException.
//		[Fact]
//		public void CreateSellOrder_SellOrderRequest_Price10001_ThrowsArgumentException()
//		{
//			SellOrderRequest testSellOrderRequest = new SellOrderRequest()
//			{
//				StockSymbol = "TEST",
//				StockName = "Test",
//				DateAndTimeOfOrder = DateTime.Now,
//				Quantity = 1,
//				Price = 10001
//			};

//			Assert.Throws<ArgumentException>(() => _stocksService.CreateSellOrder(testSellOrderRequest));
//		}

//		// When a sellOrderRequest with StockSymbol = null passed as parameter to CreateSellOrder, throw ArgumentException.
//		[Fact]
//		public void CreateSellOrder_SellOrderRequest_StockSymbolNull_ThrowsArgumentException()
//		{
//			SellOrderRequest testSellOrderRequest = new SellOrderRequest()
//			{
//				StockSymbol = null,
//				StockName = "Test",
//				DateAndTimeOfOrder = DateTime.Now,
//				Quantity = 1,
//				Price = 1
//			};

//			Assert.Throws<ArgumentException>(() => _stocksService.CreateSellOrder(testSellOrderRequest));
//		}

//		// When a sellOrderRequest with a DateAndTimeOfOrder is before the year 2000, throw ArgumentException.
//		[Fact]
//		public void CreateSellOrder_SellOrderRequest_DateAndTimeOfOrderBeforeYear2000_ThrowsArgumentException()
//		{
//			SellOrderRequest testSellOrderRequest = new SellOrderRequest()
//			{
//				StockSymbol = "TEST",
//				StockName = "Test",
//				DateAndTimeOfOrder = new DateTime(1999,12,31),
//				Quantity = 1,
//				Price = 1
//			};

//			Assert.Throws<ArgumentException>(() => _stocksService.CreateSellOrder(testSellOrderRequest));
//		}

//		// When a proper sellOrderRequest is passed as a parameter to CreateBuyOrder, returns a sellOrderResponse with a valid GUID and equal properties to the request.
//		[Fact]
//		public void CreateSellOrder_ProperSellOrderRequest_ReturnsValidSellOrderResponse()
//		{
//			SellOrderRequest testSellOrderRequest = new SellOrderRequest()
//			{
//				StockSymbol = "TEST",
//				StockName = "Test",
//				DateAndTimeOfOrder = DateTime.Now,
//				Quantity = 1,
//				Price = 1
//			};

//			SellOrderResponse outputSellOrderResponse = _stocksService.CreateSellOrder(testSellOrderRequest);

//			Assert.True(outputSellOrderResponse.SellOrderID != Guid.Empty &&
//						outputSellOrderResponse.StockSymbol == testSellOrderRequest.StockSymbol &&
//						outputSellOrderResponse.StockName == testSellOrderRequest.StockName &&
//						outputSellOrderResponse.DateAndTimeOfOrder == testSellOrderRequest.DateAndTimeOfOrder &&
//						outputSellOrderResponse.Quantity == testSellOrderRequest.Quantity &&
//						outputSellOrderResponse.Price == testSellOrderRequest.Price &&
//						outputSellOrderResponse.TradeAmount != double.NaN);
//		}
//	}
//	#endregion

//	#region StocksService.GetBuyOrders()
//	public class StocksService_GetBuyOrders_UnitTest
//	{
//		private readonly IMapper _mapper;
//		private readonly IStocksService _stocksService;

//		public StocksService_GetBuyOrders_UnitTest()
//		{
//			var serviceCollection = new ServiceCollection();
//			serviceCollection.AddAutoMapper(typeof(DomainModelToPresentationModelProfile));
//			serviceCollection.AddAutoMapper(typeof(PresentationModelToDomainModelProfile));
//			var serviceProvider = serviceCollection.BuildServiceProvider();
//			_mapper = serviceProvider.GetRequiredService<IMapper>();

//			_stocksService = new StocksService(_mapper);
//		}

//		// By default, GetBuyOrders should return an empty list of BuyOrders.
//		[Fact]
//		public void GetBuyOrders_ReturnsEmptyListAsDefault()
//		{
//			List<BuyOrderResponse> outputBuyOrderResponseList = _stocksService.GetBuyOrders();
//			Assert.Empty(outputBuyOrderResponseList);
//		}

//		// After adding buy orders using the CreateBuyOrder() method, GetBuyOrders should return a list containing the same buy orders.
//		[Fact]
//		public void GetBuyOrders_ReturnsCorrectListOfBuyOrdersAfterAddingThem()
//		{
//			BuyOrderRequest testBuyOrderRequest_1 = new BuyOrderRequest()
//			{
//				StockSymbol = "TEST1",
//				StockName = "Test1",
//				DateAndTimeOfOrder = new DateTime(2001,1,1),
//				Quantity = 1,
//				Price = 1
//			};

//			BuyOrderRequest testBuyOrderRequest_2 = new BuyOrderRequest()
//			{
//				StockSymbol = "TEST2",
//				StockName = "Test2",
//				DateAndTimeOfOrder = new DateTime(2002, 2, 2),
//				Quantity = 1,
//				Price = 1
//			};

//			BuyOrderRequest testBuyOrderRequest_3 = new BuyOrderRequest()
//			{
//				StockSymbol = "TEST3",
//				StockName = "Test3",
//				DateAndTimeOfOrder = new DateTime(2003, 3, 3),
//				Quantity = 1,
//				Price = 1
//			};

//			List<BuyOrderRequest> testBuyOrderRequestList = new List<BuyOrderRequest>()
//			{
//				testBuyOrderRequest_1, testBuyOrderRequest_2, testBuyOrderRequest_3
//			};

//			List<BuyOrderResponse> outputBuyOrderResponseList = new List<BuyOrderResponse>();

//			foreach (BuyOrderRequest request in testBuyOrderRequestList)
//			{
//				BuyOrderResponse response = _stocksService.CreateBuyOrder(request);
//				outputBuyOrderResponseList.Add(response);
//			}

//			List<BuyOrderResponse> buyOrderResponseListFromGet = _stocksService.GetBuyOrders();

//			foreach (BuyOrderResponse buyOrderFromGet in buyOrderResponseListFromGet)
//			{
//				Assert.Contains(buyOrderFromGet, outputBuyOrderResponseList);
//			}
//		}
//	}
//	#endregion

//	#region StocksService.GetSellOrders()
//	public class StocksService_GetSellOrders_UnitTest
//	{
//		private readonly IMapper _mapper;
//		private readonly IStocksService _stocksService;

//		public StocksService_GetSellOrders_UnitTest()
//		{
//			var serviceCollection = new ServiceCollection();
//			serviceCollection.AddAutoMapper(typeof(DomainModelToPresentationModelProfile));
//			serviceCollection.AddAutoMapper(typeof(PresentationModelToDomainModelProfile));
//			var serviceProvider = serviceCollection.BuildServiceProvider();
//			_mapper = serviceProvider.GetRequiredService<IMapper>();

//			_stocksService = new StocksService(_mapper);
//		}

//		// By default, GetSellOrders should return an empty list of SellOrders.
//		[Fact]
//		public void GetSellOrders_ReturnsEmptyListAsDefault()
//		{
//			List<SellOrderResponse> outputSellOrderResponseList = _stocksService.GetSellOrders();
//			Assert.Empty(outputSellOrderResponseList);
//		}

//		// After adding sell orders using the CreateSellOrder() method, GetSellOrders should return a list containing the same sell orders.
//		[Fact]
//		public void GetSellOrders_ReturnsCorrectListOfSellOrdersAfterAddingThem()
//		{
//			SellOrderRequest testSellOrderRequest_1 = new SellOrderRequest()
//			{
//				StockSymbol = "TEST1",
//				StockName = "Test1",
//				DateAndTimeOfOrder = new DateTime(2001, 1, 1),
//				Quantity = 1,
//				Price = 1
//			};

//			SellOrderRequest testSellOrderRequest_2 = new SellOrderRequest()
//			{
//				StockSymbol = "TEST2",
//				StockName = "Test2",
//				DateAndTimeOfOrder = new DateTime(2002, 2, 2),
//				Quantity = 1,
//				Price = 1
//			};

//			SellOrderRequest testSellOrderRequest_3 = new SellOrderRequest()
//			{
//				StockSymbol = "TEST3",
//				StockName = "Test3",
//				DateAndTimeOfOrder = new DateTime(2003, 3, 3),
//				Quantity = 1,
//				Price = 1
//			};

//			List<SellOrderRequest> testSellOrderRequestList = new List<SellOrderRequest>()
//			{
//				testSellOrderRequest_1, testSellOrderRequest_2, testSellOrderRequest_3
//			};

//			List<SellOrderResponse> outputSellOrderResponseList = new List<SellOrderResponse>();

//			foreach (SellOrderRequest request in testSellOrderRequestList)
//			{
//				SellOrderResponse response = _stocksService.CreateSellOrder(request);
//				outputSellOrderResponseList.Add(response);
//			}

//			List<SellOrderResponse> sellOrderResponseListFromGet = _stocksService.GetSellOrders();

//			foreach (SellOrderResponse sellOrderFromGet in sellOrderResponseListFromGet)
//			{
//				Assert.Contains(sellOrderFromGet, outputSellOrderResponseList);
//			}
//		}
//	}
//	#endregion
//}