using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Core.Repositories;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Extentions
{
    public static class InfraServices
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("OrderingConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException(connectionString);
            }


            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString,
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    sqlOptions.EnableRetryOnFailure(5);
                })
            );

            services.AddScoped<IOrderingRepo, OrderRepo>();
            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));

            return services;
        }
    }
}
