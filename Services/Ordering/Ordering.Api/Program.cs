using Ordering.API.Extensions;
using Ordering.Application.Extentions;
using Ordering.Core.Entities;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Extentions;
using Ordering.Infrastructure.SeedingData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Catalog Api",
        Version = "v1",
        Description = "this catalog microservice api",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Email = "2slamgomaa2002@gmail.com",
            Name = "eslam",


        }
    });
}
);

builder.Services.AddControllers();
builder.Services.AddInfraServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddOpenApi();

var app = builder.Build();
app.MigrateDatabase<ApplicationDbContext>((context, services) =>
{
    var logger = services.GetService<ILogger<Order>>();
    OrderSeeding.SeedAsync(context, logger).Wait();
});
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
