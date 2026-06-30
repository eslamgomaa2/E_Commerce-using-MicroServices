using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Behavior
{
    public class ValidationBehavior<TRequest,TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

    // نقوم بحقن قائمة الـ Validators الخاصة بالـ Request الحالي
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle( TRequest request, RequestHandlerDelegate<TResponse> next,  CancellationToken cancellationToken)
        {
            // إذا لم يكن هناك أي شروط تحقق لهذا الـ Request، مرر الطلب للخطوة التالية
            if (!_validators.Any())
            {
                return await next();
            }

            // تشغيل التحقق بشكل غير متزامن (Async) لجميع الـ Validators المرتبطة
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)) );

            // تجميع كافة الأخطاء إن وجدت
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            // إذا كان هناك أخطاء، ارمِ استثناء التحقق واقطع خط سير الطلب
            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }

            // إذا كان كل شيء سليماً، أكمل السير إلى الـ Handler
            return await next();
        }
    }
}
