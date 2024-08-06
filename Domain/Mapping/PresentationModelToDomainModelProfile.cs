using AutoMapper;
using StocksApp.Domain.Models;
using StocksApp.Presentation.Models;

namespace StocksApp.Domain.Mapping
{
	public class PresentationModelToDomainModelProfile : Profile
	{
		public PresentationModelToDomainModelProfile()
		{
			CreateMap<BuyOrderRequest, BuyOrder>()
				.ForMember(dest => dest.TradeAmount, opt => opt.MapFrom(
					src => src.Quantity * src.Price));
			CreateMap<SellOrderRequest, SellOrder>()
				.ForMember(dest => dest.TradeAmount, opt => opt.MapFrom(
					src => src.Quantity * src.Price));
		}
	}
}
