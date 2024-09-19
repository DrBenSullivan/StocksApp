using AutoMapper;
using StocksApp.Domain.Models;
using StocksApp.Presentation.Models;

namespace StocksApp.Domain.Mapping
{
    public class DomainModelToPresentationModelProfile : Profile
    {
        public DomainModelToPresentationModelProfile()
        {
            CreateMap<BuyOrder, BuyOrderResponse>();
            CreateMap<SellOrder, SellOrderResponse>();
        }
    }
}
