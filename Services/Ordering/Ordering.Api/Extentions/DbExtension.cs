using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using System;

namespace Ordering.API.Extensions
{
    public static class DbExtension
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder)
            where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation($"Started db migration: {typeof(TContext).Name}");

                    // سياسة إعادة المحاولة لحماية التطبيق عند التشغيل داخل Docker
                    var retry = Policy.Handle<SqlException>()
                        .WaitAndRetry(
                            retryCount: 5,
                            sleepDurationProvider: retryAttempts => TimeSpan.FromSeconds(Math.Pow(2, retryAttempts)),
                            onRetry: (exception, span, count) =>
                            {
                                logger.LogWarning($"Retrying because of {exception.Message} {span}");
                            });

                    // تنفيذ الـ Migration والـ Seeder داخل الـ Retry Policy
                    retry.Execute(() => CallSeeder(seeder, context, services));

                    logger.LogInformation($"Finished db migration: {typeof(TContext).Name}");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occurred while migrating the database used on context {typeof(TContext).Name}");
                    throw;
                }
            }

            return host; // إرجاع الـ host للحفاظ على نمط الـ Chaining في ملف Program.cs
        }

        // دالة الـ CallSeeder المسؤولة عن تطبيق التحديثات وحقن البيانات
        private static void CallSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services)
            where TContext : DbContext
        {
            // 1. تطبيق الـ Migrations بشكل فعلي على قاعدة البيانات
            context.Database.Migrate();

            // 2. تشغيل دالة الـ Seeder لإدخال البيانات الافتراضية
            seeder(context, services);
        }
    }
}