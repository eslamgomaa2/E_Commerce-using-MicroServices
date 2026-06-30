using FluentValidation;
using Ordering.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Validators
{
    public class UpdateOrderCommandValidator:AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.CardNumber).NotEmpty().WithMessage("Card number and address line are required.");
            RuleFor(x => x.CardName).NotEmpty().WithMessage("Card name is required.");
            RuleFor(x => x.AddressLine).NotEmpty().WithMessage("Address line is required.");
            RuleFor(x => x.TotalPrice).GreaterThan(0).WithMessage("Total price must be greater than zero.");
            RuleFor(x => x.EmailAddress).NotEmpty().EmailAddress().WithMessage("A valid email address is required.");
            RuleFor(x => x.Expiration).NotEmpty().WithMessage("Expiration date is required.");
            RuleFor(x => x.CVV).NotEmpty().WithMessage("CVV is required.");
        }
    }
}
