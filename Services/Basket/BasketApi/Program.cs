using BasketApi.GRPC_Services;
using BasketApi.Mapper;
using BasketApi.Repositories;
using DiscountGrpc.Protos;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddAutoMapper(typeof(BasketProfile).Assembly);


builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(
    option =>
    {
        option.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]);
    });

builder.Services.AddScoped<DiscountGrpcService>();


builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});


builder.Services.AddMassTransit(cnf =>
{
    cnf.UsingRabbitMq((ctx, conf) =>
    {
        conf.Host(builder.Configuration.GetValue<string>("EventBusSettings:HostAddress"));
    });
});




var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
