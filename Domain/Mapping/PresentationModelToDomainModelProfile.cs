using AutoMapper;
using StocksApp.Domain.Models;
using StocksApp.Presentation.Models;

namespace StocksApp.Domain.Mapping
{
	public class PresentationModelToDomainModelProfile : Profile
	{
		public PresentationModelToDomainModelProfile()
		{
			CreateMap<BuyOrderRequest, BuyOrder>();
			CreateMap<SellOrderRequest, SellOrder>();
		}
	}
}
