using AutoMapper;
using EventBusMessages.Events;
using OrderingApplication.Features.Orders.Commands.CheckoutOrder;

namespace OrderingApi.Mapping;

public class OrderingProfile : Profile
{
    public OrderingProfile()
    {
        CreateMap<CheckoutOrderCommandRequest, BasketCheckoutEvent>().ReverseMap();

    }
}
