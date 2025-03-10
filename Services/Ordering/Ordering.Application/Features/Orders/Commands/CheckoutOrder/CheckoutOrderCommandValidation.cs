using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandValidation : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidation()
        {
            RuleFor(c => c.UserName)
                .NotEmpty().WithMessage("{UserName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{UserName} must not exeed 50 character");

            RuleFor(c => c.EmailAddress)
                .NotEmpty().WithMessage("{EmailAddress} is required.");

            RuleFor(c=> c.TotalPrice)
                .NotEmpty().WithMessage("{TotalPrice} is required.")
                .GreaterThan(0).WithMessage("{TotalPrice} should be greater than 0");
        }
    }
}
