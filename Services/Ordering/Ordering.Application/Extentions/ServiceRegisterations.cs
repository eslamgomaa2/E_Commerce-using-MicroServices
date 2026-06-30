using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Behavior;
using Ordering.Application.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Extentions
{
    public static class ServiceRegisterations
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(OrderProfile).Assembly);
            });
            services.AddMediatR(cnf => cnf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            // تسجيل الـ Validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            // تسجيل الـ Pipeline Behaviors
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnHandledExceptionBehavior<,>));
            return services;
        }
    }
}
