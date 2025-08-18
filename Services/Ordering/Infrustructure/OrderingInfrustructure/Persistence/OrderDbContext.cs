using Microsoft.EntityFrameworkCore;
using OrderingDomain.Common;
using OrderingDomain.Entitiy;

namespace OrderingInfrastructure.Persistence;

public class OrderDbContext :DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {
        
    }

    public DbSet<Order> Orders { get; set; }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries<EntityBase>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreateDate = DateTime.Now;
                    entry.Entity.CreatedBy = "Kamyar";
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedDate = DateTime.Now;
                    entry.Entity.LastModifiedBy = "Kamyar";
                    break;

            }
        }

        return base.SaveChanges();
    }

}
