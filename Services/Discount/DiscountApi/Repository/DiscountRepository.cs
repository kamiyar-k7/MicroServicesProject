using Dapper;
using DiscountApi.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Npgsql;

namespace DiscountApi.Repository;

public class DiscountRepository : IDiscountRepository
{

    #region Ctor
    private readonly IConfiguration _configuration;
    public DiscountRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    #endregion

    #region Get

    public async Task<Coupon> GetDiscount(string productName)
    {
        var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
            ("SELECT * FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });


        return coupon ?? new Coupon()
        {
            ProductName = "No Discount",
            Amount = 0,
            Description = "No Discount",

        };
    }

    #endregion

    #region Create
    public async Task<bool> CreateDiscount(Coupon coupon)
    {

        var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        var created = await connection.ExecuteAsync
            ("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)"
            , new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

        return created == 1 ? true : false;


    }
    #endregion

    #region Update
    public async Task<bool> UpdateDiscount(Coupon coupon)
    {

        var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        var updated = await connection.ExecuteAsync("UPDATE Coupon SET ProductName = @ProductName , Description = @Description , Amount = @Amount WHERE id = @Id", new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount  , Id = coupon.Id});

        return updated == 1 ? true : false;
    }
    #endregion

    #region Delete
    public async Task<bool> DeleteDiscount(string productName)
    {
        var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        var deleted = await connection.ExecuteAsync
            ("DELETE FROM Coupon WHERE ProductName=@ProductName ", new { ProductName = productName });

        return deleted == 1 ? true : false;
    }
    #endregion



}