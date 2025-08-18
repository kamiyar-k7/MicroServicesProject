
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderingDomain.Entitiy;


namespace OrderingInfrastructure.Persistence;

public class OrderContextSeed
{
    public static async Task SeedAsync(OrderDbContext context, ILogger<OrderContextSeed> logger)
    {
        if (!await context.Orders.AnyAsync())
        {
            await context.Orders.AddRangeAsync(GetPreConfiguredOrders());
            await context.SaveChangesAsync();
            logger.LogInformation("data seed section configured");

        }
    }

    public static IEnumerable<Order> GetPreConfiguredOrders()
    {

        return new List<Order>
        {
            new Order
            {
                FirstName = "kamyar",
                LastName =  "khedri",
                UserName = "kamyar",
                EmailAddres= "kamyarkhedri2004@gmail.com",
                City = "Abadan",
                Country = "Iran",
                TotalPrice = 1000,
                BankName = "bank",
                CreateDate = DateTime.Now,
                CreatedBy = "kamyar",
                LastModifiedBy = " kamyar",
                PaymentMethod = 1,
                RefCode = "323",
                



            }
        };
    }
}
