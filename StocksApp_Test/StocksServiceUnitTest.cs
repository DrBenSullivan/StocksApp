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
using FluentAssertions.Specialized;
using FluentAssertions.Common;

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
			var testBuyOrderRequest = _fixture.Create<BuyOrderRequest>();

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

	#region StocksService.CreateSellOrder()
	public class StocksService_CreateSellOrder_UnitTest
	{
		private readonly Mock<IStocksRepository> _mockStocksRepository;
		private readonly IStocksRepository _stocksRepository;
		private readonly IStocksService _stocksService;
		private readonly IMapper _mapper;	
		private readonly IFixture _fixture;

		public StocksService_CreateSellOrder_UnitTest()
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

		// When null passed as parameter to CreateSellOrder, throw ArgumentNullException.
		[Fact]
		public void CreateSellOrder_NullParameter_ThrowsArgumentNullException()
		{
			Func<Task> action = async () => await _stocksService.CreateSellOrder(null);

			action.Should().ThrowAsync<ArgumentNullException>();
		}

		// When a sellOrderRequest with Quantity = 0 (specified range 1-100000) passed as paramter to CreateSellOrder, throw ArgumentException.
		[Fact]
		public void CreateSellOrder_SellOrderRequest_QuantityNull_ThrowsArgumentException()
		{
			var testSellOrderRequest = _fixture.Build<SellOrderRequest>()
				.With(r => r.Quantity, 0)
				.Create();

			Func<Task> action = async () => await _stocksService.CreateSellOrder(testSellOrderRequest);

			action.Should().ThrowAsync<ArgumentException>();
		}

		// When a sellOrderRequest with Quantity = 100001 (specified range 1-100000) passed as parameter to CreateSellOrder, throw ArgumentException.
		[Fact]
		public void CreateSellOrder_SellOrderRequest_Quantity100001_ThrowsArgumentException()
		{
			var testSellOrderRequest = _fixture.Build<SellOrderRequest>()
				.With(r => r.Quantity, 100001)
				.Create();

			Func<Task> action = async () => await _stocksService.CreateSellOrder(testSellOrderRequest);

			action.Should().ThrowAsync<ArgumentException>();
		}

		// When a sellOrderRequest with Price = 0 (specified range 0-10000) passed as parameter to CreateSellOrder, throw ArgumentException.
		[Fact]
		public void CreateSellOrder_SellOrderRequest_Price0_ThrowsArgumentException()
		{
			var testSellOrderRequest = _fixture.Build<SellOrderRequest>()
				.With(r => r.Price, 0)
				.Create();

			Func<Task> action = async () => await _stocksService.CreateSellOrder(testSellOrderRequest);

			action.Should().ThrowAsync<ArgumentException>();
		}

		// When a sellOrderRequest with Price = 10001 (specified range 0-10000) passed as parameter to CreateSellOrder, throw ArgumentException.
		[Fact]
		public void CreateSellOrder_SellOrderRequest_Price10001_ThrowsArgumentException()
		{
			var testSellOrderRequest = _fixture.Build<SellOrderRequest>()
				.With(r => r.Price, 10001)
				.Create();

			Func<Task> action = async () => await _stocksService.CreateSellOrder(testSellOrderRequest);

			action.Should().ThrowAsync<ArgumentException>();
		}

		// When a sellOrderRequest with StockSymbol = null passed as parameter to CreateSellOrder, throw ArgumentException.
		[Fact]
		public void CreateSellOrder_SellOrderRequest_StockSymbolNull_ThrowsArgumentException()
		{
			var testSellOrderRequest = _fixture.Build<SellOrderRequest>()
				.With(r => r.StockSymbol, null as string)
				.Create();

			Func<Task> action = async () => await _stocksService.CreateSellOrder(testSellOrderRequest);

			action.Should().ThrowAsync<ArgumentException>();
		}

		// When a sellOrderRequest with a DateAndTimeOfOrder is before the year 2000, throw ArgumentException.
		[Fact]
		public void CreateSellOrder_SellOrderRequest_DateAndTimeOfOrderBeforeYear2000_ThrowsArgumentException()
		{
			var testSellOrderRequest = _fixture.Build<SellOrderRequest>()
				.With(r => r.DateAndTimeOfOrder, DateTime.Parse("31-12-1999"))
				.Create();

			Func<Task> action = async () => await _stocksService.CreateSellOrder(testSellOrderRequest);

			action.Should().ThrowAsync<ArgumentException>();
		}

		// When a proper sellOrderRequest is passed as a parameter to CreateSellOrder, returns a sellOrderResponse with a valid GUID and equal properties to the request.
		[Fact]
		public async Task CreateSellOrder_ProperSellOrderRequest_ReturnsValidSellOrderResponse()
		{
			var testSellOrderRequest = _fixture.Create<SellOrderRequest>();

			var expectedSellOrder = _mapper.Map<SellOrder>(testSellOrderRequest);
			var expectedSellOrderResponse = _mapper.Map<SellOrderResponse>(expectedSellOrder);

			_mockStocksRepository.Setup
				(p => p.CreateSellOrder(It.IsAny<SellOrder>()))
				.ReturnsAsync(expectedSellOrder);

            var outputSellOrderResponse = await _stocksService.CreateSellOrder(testSellOrderRequest);

			outputSellOrderResponse.Should().Be(expectedSellOrderResponse);
		}
	}
	#endregion

	#region StocksService.GetBuyOrders()
	public class StocksService_GetBuyOrders_UnitTest
	{
		private readonly Mock<IStocksRepository> _mockStocksRepository;
		private readonly IStocksRepository _stocksRepository;
		private readonly IStocksService _stocksService;
		private readonly IMapper _mapper;	
		private readonly IFixture _fixture;

		public StocksService_GetBuyOrders_UnitTest()
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

		// By default, GetBuyOrders should return an empty list of BuyOrders.
		[Fact]
		public async Task GetBuyOrders_ReturnsEmptyListAsDefault()
		{
			_mockStocksRepository
				.Setup(r => r.GetBuyOrders())
				.ReturnsAsync([]);

			var result = await _stocksService.GetBuyOrders();
			var expected = new List<BuyOrderResponse>();

			result.Should().BeEquivalentTo(expected);
		}

		// After adding buy orders using the CreateBuyOrder() method, GetBuyOrders should return a list containing the same buy orders.
		[Fact]
		public async Task GetBuyOrders_ReturnsCorrectListOfBuyOrdersAfterAddingThem()
		{
			var buyOrders = _fixture.CreateMany<BuyOrder>().ToList();
			var buyOrderResponses = buyOrders.Select(order => _mapper.Map<BuyOrderResponse>(order)).ToList();

			_mockStocksRepository
				.Setup(r => r.GetBuyOrders())
				.ReturnsAsync(buyOrders);

			var result = await _stocksService.GetBuyOrders();

			result.Should().BeEquivalentTo(buyOrderResponses);
		}
	}
	#endregion

	#region StocksService.GetSellOrders()
	public class StocksService_GetSellOrders_UnitTest
	{
		private readonly Mock<IStocksRepository> _mockStocksRepository;
		private readonly IStocksRepository _stocksRepository;
		private readonly IStocksService _stocksService;
		private readonly IMapper _mapper;	
		private readonly IFixture _fixture;

		public StocksService_GetSellOrders_UnitTest()
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

		// By default, GetSellOrders should return an empty list of SellOrders.
		[Fact]
		public async Task GetSellOrders_ReturnsEmptyListAsDefault()
		{
			_mockStocksRepository
				.Setup(r => r.GetSellOrders())
				.ReturnsAsync([]);

			var result = await _stocksService.GetSellOrders();
			var expected = new List<SellOrderResponse>();

			result.Should().BeEquivalentTo(expected);
		}

		// After adding sell orders using the CreateSellOrder() method, GetSellOrders should return a list containing the same sell orders.
		[Fact]
		public async Task GetSellOrders_ReturnsCorrectListOfSellOrdersAfterAddingThem()
		{
			var sellOrders = _fixture.CreateMany<SellOrder>().ToList();
			var sellOrderResponses = sellOrders.Select(order => _mapper.Map<SellOrderResponse>(order)).ToList();

			_mockStocksRepository
				.Setup(r => r.GetSellOrders())
				.ReturnsAsync(sellOrders);

			var result = await _stocksService.GetSellOrders();

			result.Should().BeEquivalentTo(sellOrderResponses);
		}
	}
	#endregion
}