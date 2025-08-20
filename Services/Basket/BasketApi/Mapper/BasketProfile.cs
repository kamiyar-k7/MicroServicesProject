using AutoMapper;
using BasketApi.Entities;
using EventBusMessages.Events;

namespace BasketApi.Mapper;

public class BasketProfile : Profile
{

    public BasketProfile()
    {

        CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();

    }
}
