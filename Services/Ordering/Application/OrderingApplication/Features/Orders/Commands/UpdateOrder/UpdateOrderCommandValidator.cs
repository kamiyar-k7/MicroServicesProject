
using FluentValidation;

namespace OrderingApplication.Features.Orders.Commands.UpdateOrder;

public  class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommandRequest>
{

    public UpdateOrderCommandValidator()
    {
        RuleFor(r => r.UserName).NotEmpty().WithMessage("{UserName} IS required").
                               NotNull().
                               MaximumLength(50).WithMessage("{UserName} must not be more then 50 characters");

        RuleFor(r => r.EmailAddres).NotEmpty().WithMessage(" {Email} is required");

        RuleFor(r => r.TotalPrice).NotEmpty().WithMessage("{TotalPrice} is required").
                                   GreaterThan(0).WithMessage("{TotalPrice} should be greater then 0");
    }

}
