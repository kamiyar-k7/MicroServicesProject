using AutoMapper;
using EventBusMessages.Events;
using MassTransit;
using MediatR;
using OrderingApplication.Features.Orders.Commands.CheckoutOrder;

namespace OrderingApi.EventBusConsumer;

public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
{

    #region Ctor
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ILogger<BasketCheckoutConsumer> _logger;
    public BasketCheckoutConsumer(IMapper mapper, IMediator mediator, ILogger<BasketCheckoutConsumer> logger)
    {
        _mapper = mapper;
        _mediator = mediator;
        _logger = logger;
    }
    #endregion


    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        var command = _mapper.Map<CheckoutOrderCommandRequest>(context.Message);
      var res  = await _mediator.Send(command);
        _logger.LogInformation($"order consume successfully and order id is : {res}");
    }
}
