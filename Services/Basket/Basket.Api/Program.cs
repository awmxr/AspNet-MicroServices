using Basket.Api.GrpcServices;
using Basket.Api.Repositories;
using Discount.Grpc.Protos;
using MassTransit;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
//builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(
//    options =>
//    {
//        options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]);
//    });

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>
           (o => o.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]
                                    ?? throw new Exception("Configration Not found")));
builder.Services.AddScoped<DiscountGrpcService>();
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx,conf) => {
        conf.Host(builder.Configuration.GetValue<string>("EventBusSettings:HostAddress"));
});
});

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
//builder.Services.AddMassTransitHostedService();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
