using MediatR;
using Microsoft.Extensions.Logging;
using OrderingApplication.Contracts.Persistence;
using OrderingApplication.Exceptions;
using OrderingDomain.Entitiy;

namespace OrderingApplication.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommandRequest>
{

    #region Ctor

    private readonly ILogger<DeleteOrderCommandHandler> _logger;
    private readonly IOrderRepository _orderRepository;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository, ILogger<DeleteOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    #endregion

    public async Task<Unit> Handle(DeleteOrderCommandRequest request, CancellationToken cancellationToken)
    {
        var entity = await _orderRepository.GetByIdAsync(request.Id);

        if (entity == null)
        {
            _logger.LogError("entity was null");
            throw new NotFoundException(nameof(Order), request.Id);
        }
        else
        {

            await _orderRepository.DeleteAsync(entity);
            _logger.LogInformation("order deleted successfully");
        }


        return Unit.Value;
    }
}