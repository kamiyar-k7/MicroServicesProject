using EventBusMessages.Common;
using MassTransit;
using OrderingApi.EventBusConsumer;
using OrderingApi.Extensions;
using OrderingApi.Mapping;
using OrderingApplication.ServiceRegistration;
using OrderingInfrastructure.Persistence;
using OrderingInfrastructure.ServiceRegistration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

#region Services Registration0
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddAutoMapper(typeof(OrderingProfile).Assembly);
#endregion


#region Mass transit

builder.Services.AddMassTransit(cnf =>
{
    cnf.AddConsumer<BasketCheckoutConsumer>();

    cnf.UsingRabbitMq((ctx, conf) =>
    {
        conf.Host(builder.Configuration.GetValue<string>("EventBusSettings:HostAddress"));
        conf.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c =>
        {
            c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
        });
    });
});

builder.Services.AddScoped<BasketCheckoutConsumer>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MigrateDatabase<OrderDbContext>((context , services) =>
{
    var logger = services.GetService<ILogger<OrderContextSeed>>();
    OrderContextSeed.SeedAsync(context, logger).Wait();

});

app.UseAuthorization();

app.MapControllers();

app.Run();
