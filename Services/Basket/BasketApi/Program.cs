using BasketApi.GRPC_Services;
using BasketApi.Repositories;
using DiscountGrpc.Protos;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();


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
