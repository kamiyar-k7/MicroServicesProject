using DiscountGrpc.Entities;

namespace DiscountGrpc.Repository;

public interface IDiscountRepository
{
    Task<Coupon> GetDiscount(string prodcutName);
    Task<bool> CreateDiscount(Coupon coupon);
    Task<bool> UpdateDiscount(Coupon coupon);
    Task<bool> DeleteDiscount(string prodcutName);

}
