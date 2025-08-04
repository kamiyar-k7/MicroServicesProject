﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace DiscountApi.Extentions;

public static class HostExtentions
{
    public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
    {

        int retryForAvailability = retry.Value;



        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
           var configuration = services.GetRequiredService<IConfiguration>();
            var logger = services.GetRequiredService<ILogger<TContext>>();

            // migrate
            try
            {
                logger.LogInformation("migrate database");
              using  var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                connection.Open();

                using var command = new NpgsqlCommand
                {
                    Connection = connection,
                };

                command.CommandText = "DROP TABLE IF EXISTS Coupon";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY,
                                                            ProductName VARCHAR(200) NOT NULL,
                                                            Description TEXT,
                                                            Amount INT)";

                command.ExecuteNonQuery();

                //seed
                command.CommandText = "INSERT INTO Coupon(ProductName , Description , Amount) VALUES ('Laptop' , 'This is Laptop' , 20)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO Coupon(ProductName , Description , Amount) VALUES ('Mobile' , 'This is Mobile' , 10)";
                command.ExecuteNonQuery();

                logger.LogInformation("migration completed");
            }
            catch (NpgsqlException ex)
            {

                logger.LogError("en error occured" + ex.Message);
                if (retryForAvailability < 50) 
                {
                    retryForAvailability++;
                    Thread.Sleep(200);
                    MigrateDatabase<TContext>(host, retryForAvailability);
                }
            }
        }

        return host;
    }

   
    
}
