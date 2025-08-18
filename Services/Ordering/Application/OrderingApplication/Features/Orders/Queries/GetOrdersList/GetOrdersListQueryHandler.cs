
using AutoMapper;
using MediatR;
using OrderingApplication.Contracts.Persistence;

namespace OrderingApplication.Features.Orders.Queries.GetOrdersList;
                                                             //handle tihis request      return this data
public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQueryRequest, List<OrdersViewModel>>
{

    #region Ctor

    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    #endregion

    #region Handler

    public async Task<List<OrdersViewModel>> Handle(GetOrdersListQueryRequest request, CancellationToken cancellationToken)
    {
        var ordersList = await _orderRepository.GetOrdersByUserName(request.UserName);

        return _mapper.Map<List<OrdersViewModel>>(ordersList);

    }
    #endregion

}
