using Basket.Application.gRPCServices;
using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Core.Repositories;
using Basket.Infrastructure.Repositories;
using Discount.Grpc.Proto;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Basket Api",
        Version = "v1",
        Description = "this Basket microservice api",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Email = "2slamgomaa2002@gmail.com",
            Name = "eslam",
        }
    });
});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});

builder.Services.AddAutoMapper(cfg => { }, typeof(BasketMapper).Assembly);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    Assembly.GetExecutingAssembly(),
    Assembly.GetAssembly(typeof(GetBasketByUserNameQuery))));

builder.Services.AddApiVersioning(cfg =>
{
    cfg.ReportApiVersions = true;
    cfg.AssumeDefaultVersionWhenUnspecified = true;
    cfg.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
});

builder.Services.AddScoped < IBasketRepo, BasketRepo > ();
builder.Services.AddScoped < DiscountgRPC_Services > ();
builder.Services.AddGrpcClient < DiscountProtoService.DiscountProtoServiceClient> (opt=>opt.Address=new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]));
builder.Services.AddControllers();


builder.Services.AddOpenApi();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();