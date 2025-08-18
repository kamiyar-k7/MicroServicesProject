

using FluentValidation;
using MediatR;
using ValidationException = OrderingApplication.Exceptions.ValidationException;

namespace OrderingApplication.Behaviors;

public class ValidationBehaviors<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{

    #region Ctor

    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviors(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    #endregion

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(_validators.Select(x => x.ValidateAsync(context, cancellationToken)));

            var failures = validationResults.SelectMany(f => f.Errors).Where(e => e != null).ToList();

            if (failures.Count() > 0) 
            {
                throw new ValidationException(failures);
            }

        }
        return await next();
    }
}
