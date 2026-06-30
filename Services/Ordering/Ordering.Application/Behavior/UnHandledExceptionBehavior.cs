using MediatR;
using Microsoft.Extensions.Logging;

namespace Ordering.Application.Behavior
{
    internal class UnHandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public UnHandledExceptionBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                // تمرير الطلب للخطوة التالية في خط السير
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;

                // تسجيل الخطأ غير المتوقع في الـ Log
                _logger.LogInformation($"Unhandled exception occured with request name :{requestName}, {request}");

                // إعادة رمي الاستثناء
                throw;
            }
        }
    }
}
