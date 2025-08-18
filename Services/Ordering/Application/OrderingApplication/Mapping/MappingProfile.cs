
using AutoMapper;
using OrderingApplication.Features.Orders.Commands.CheckoutOrder;
using OrderingApplication.Features.Orders.Commands.UpdateOrder;
using OrderingApplication.Features.Orders.Queries.GetOrdersList;
using OrderingDomain.Entitiy;

namespace OrderingApplication.Mapping;

public class MappingProfile : Profile
{

    public MappingProfile()
    {
        CreateMap<Order, OrdersViewModel>().ReverseMap();
        CreateMap<Order , CheckoutOrderCommandRequest>().ReverseMap();
        CreateMap<Order , UpdateOrderCommandRequest>().ReverseMap();
    }

}
