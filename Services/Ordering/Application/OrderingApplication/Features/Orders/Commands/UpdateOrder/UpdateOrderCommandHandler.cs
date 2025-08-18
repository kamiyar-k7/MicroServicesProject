
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderingApplication.Contracts.Persistence;
using OrderingApplication.Exceptions;
using OrderingDomain.Entitiy;

namespace OrderingApplication.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommandRequest>
{

    #region Ctor
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<UpdateOrderCommandHandler> _logger;
    public UpdateOrderCommandHandler(IMapper mapper, ILogger<UpdateOrderCommandHandler> logger, IOrderRepository orderRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _orderRepository = orderRepository;
    }

    #endregion


    public async Task<Unit> Handle(UpdateOrderCommandRequest request, CancellationToken cancellationToken)
    {
        var orderForUpdate = await _orderRepository.GetByIdAsync(request.Id);
        if (orderForUpdate == null) 
        {
            _logger.LogInformation("order does not exist");
            throw new NotFoundException(nameof(Order) , request.Id);

        }
        _mapper.Map(request , orderForUpdate , typeof(UpdateOrderCommandRequest) ,typeof(Order));

        await _orderRepository.UpdateAsync(orderForUpdate);
        
        _logger.LogInformation($"{request.Id} Updated");
        return Unit.Value;
    }
}
