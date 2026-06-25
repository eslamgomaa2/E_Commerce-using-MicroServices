using DisCount.Api.Services;
using DisCount.Application._02Queries;
using DisCount.Application.Mapper;
using DisCount.Core.Repositories;
using DisCount.InfraStructure.Extentions;
using DisCount.InfraStructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// ✅ لازم تضيف السطر ده عشان gRPC يشتغل
builder.Services.AddGrpc();

builder.Services.AddAutoMapper(cfg => { }, typeof(DiscountProfile).Assembly);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    Assembly.GetExecutingAssembly(),
    Assembly.GetAssembly(typeof(GetDiscountQuery))));

builder.Services.AddScoped<IDiscountRepo, DiscountRepo>();
builder.Services.AddControllers();

// لو إنت مش على .NET 9، استخدم AddSwaggerGen بدل AddOpenApi
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
}

app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<DiscountServices>();
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("communication with grpc");
    });
});

app.MapControllers();

// ✅ تأكد إن ApplyMigrations مش بترجع null
app.ApplyMigrations();

app.Run();