using FluentValidation;
using Ordering.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Validators
{
   /* استخدم Data Annotations إذا كان مشروعك صغيراً، أو كانت الشروط بسيطة جداً لا تتعدى(مطلوب / حد أقصى للحروف).
واستخدم FluentValidation إذا كنت تبني مشروعاً كبيراً، أو تتبع نمط هندسة برمجيات نظيفة(Clean Architecture)، حيث تريد فصل منطق العمل تماماً عن البيانات.
  */  public class CreateOrderCommandValidator:AbstractValidator<AddOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x =>x.CardNumber ).NotEmpty().WithMessage("Card number and address line are required.");
            RuleFor(x => x.CardName).NotEmpty().WithMessage("Card name is required.");
            RuleFor(x => x.AddressLine).NotEmpty().WithMessage("Address line is required.");
            RuleFor(x => x.TotalPrice).GreaterThan(0).WithMessage("Total price must be greater than zero.");
            RuleFor(x => x.EmailAddress).NotEmpty().EmailAddress().WithMessage("A valid email address is required.");
            RuleFor(x => x.Expiration).NotEmpty().WithMessage("Expiration date is required.");
            RuleFor(x => x.CVV).NotEmpty().WithMessage("CVV is required.");
        }
    }
}
