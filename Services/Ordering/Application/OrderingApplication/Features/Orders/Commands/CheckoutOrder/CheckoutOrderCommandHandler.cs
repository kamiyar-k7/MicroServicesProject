
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderingApplication.Contracts.Infrastructure;
using OrderingApplication.Contracts.Persistence;
using OrderingDomain.Entitiy;

namespace OrderingApplication.Features.Orders.Commands.CheckoutOrder;

public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommandRequest, int>
{
    #region Ctor
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;
    private readonly IEmailService _emailService;
    private readonly ILogger<CheckoutOrderCommandHandler> _logger;
    public CheckoutOrderCommandHandler(IMapper mapper,
                                       IOrderRepository orderRepository,
                                       IEmailService emailService,
                                       ILogger<CheckoutOrderCommandHandler> logger)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
        _emailService = emailService;
        _logger = logger;
    }


    #endregion

    #region Handler
    public async Task<int> Handle(CheckoutOrderCommandRequest request, CancellationToken cancellationToken)
    {

        var orderEntity = _mapper.Map<Order>(request);
        var newOrder =   await _orderRepository.AddAsync(orderEntity);

        _logger.LogInformation($"Order with id '{newOrder.Id}' is successfully registered.");

        //send email

        return newOrder.Id;

    }
    #endregion

    private async Task SendMail(Order order)
    {
        try
        {
            await _emailService.SendEmailAsync(new Models.Email
            {
                //implement email
            });
        }
        catch (Exception ex)
        {

            _logger.LogInformation("Email Has Not Been Sent");
        }
    }

}

    
