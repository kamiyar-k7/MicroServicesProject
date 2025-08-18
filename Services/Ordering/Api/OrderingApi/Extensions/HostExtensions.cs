using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace OrderingApi.Extensions;

public static class HostExtensions
{
    public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder, int? retry = 0) where TContext : DbContext
    {

        int retryForAvailavility = retry.Value;

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<TContext>>();
            var context = services.GetService<TContext>();

            try
             {
                logger.LogInformation("migrating started for sql server");

                InvokeSeeder(seeder, context, services);

                logger.LogInformation("migrating finished successfully for sql server");

            }
            catch (SqlException ex)
            {
                logger.LogError("error occurd for migratinf sql server");

                if (retryForAvailavility < 50)
                {
                    retryForAvailavility++;
                    Thread.Sleep(200);
                    MigrateDatabase<TContext>(host, seeder, retryForAvailavility);

                }
                throw;
            }
        }

        return host;

    }


    private static void InvokeSeeder<TContext>
        (Action<TContext, IServiceProvider> seeder,
        TContext context,
        IServiceProvider services)
        where TContext : DbContext
    {

        context.Database.Migrate();
        seeder(context, services);
    }
}
