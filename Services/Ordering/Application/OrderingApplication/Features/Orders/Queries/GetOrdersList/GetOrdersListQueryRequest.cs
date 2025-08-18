
using MediatR;

namespace OrderingApplication.Features.Orders.Queries.GetOrdersList;

public class GetOrdersListQueryRequest : IRequest<List<OrdersViewModel>>
{
    public string UserName { get; set; }

    public GetOrdersListQueryRequest(string username)
    {
        UserName = username;
    }


}
