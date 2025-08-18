
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace OrderingApplication.Behaviours;

public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    #region Ctor

  
    private readonly ILogger<UnhandledExceptionBehavior<TRequest, TResponse>> _logger;

    public UnhandledExceptionBehavior(ILogger<UnhandledExceptionBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    #endregion

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
      

        catch (Exception ex)
        {

            var requestName = typeof(TRequest).Name;
            _logger.LogError(ex.Message , $"Apllication Request : Unhadled Exception For Request {requestName} , {request}");
            throw;
        }
    }
}
