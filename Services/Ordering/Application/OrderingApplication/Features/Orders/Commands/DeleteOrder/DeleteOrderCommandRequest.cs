
using MediatR;

namespace OrderingApplication.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandRequest : IRequest
{

	public int Id { get; set; }

}
