
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderingApplication.Contracts.Infrastructure;
using OrderingApplication.Contracts.Persistence;
using OrderingInfrastructure.Mail;
using OrderingInfrastructure.Persistence;
using OrderingInfrastructure.Repositories;

namespace OrderingInfrastructure.ServiceRegistration;

public static class InfrastructureServices
{

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services , IConfiguration configuration)
    {

        services.AddDbContext<OrderDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString"));

        });

        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }

}
