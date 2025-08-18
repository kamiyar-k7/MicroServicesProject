
using Microsoft.EntityFrameworkCore;
using OrderingApplication.Contracts.Persistence;
using OrderingDomain.Entitiy;
using OrderingInfrastructure.Persistence;

namespace OrderingInfrastructure.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{

    public OrderRepository(OrderDbContext dbContext) : base(dbContext)  
    {
        
    }


    public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
    {
        return await _dbContext.Orders.Where(o => o.UserName == userName).ToListAsync();

    }
}
