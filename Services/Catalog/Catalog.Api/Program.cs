using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data.Context;
using Catalog.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddAutoMapper(typeof(ProductMapperProfile).Assembly);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(GetProuductByIdQuery))));
builder.Services.AddScoped<ICatalogContext, CatalogContext>();
builder.Services.AddScoped<IProductRepo, ProductRepository>();
builder.Services.AddScoped<IProducttypeRepo, ProductRepository>();
builder.Services.AddScoped<IBrandRepo, ProductRepository>();
builder.Services.AddApiVersioning(cfg =>
{
    cfg.ReportApiVersions = true;
    cfg.AssumeDefaultVersionWhenUnspecified = true;
    cfg.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
});
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
