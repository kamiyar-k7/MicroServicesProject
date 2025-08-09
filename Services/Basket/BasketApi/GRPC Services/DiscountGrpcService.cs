using DiscountGrpc.Protos;

namespace BasketApi.GRPC_Services;

public class DiscountGrpcService
{
    #region Ctor

    private readonly DiscountProtoService.DiscountProtoServiceClient _client;

    public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient client)
    {
        _client = client;
    }
    #endregion

    #region Get Discount

    public async Task<CouponModel> GetDiscount(string productName)
    {
        var discountRequest = new GetDiscountRequest { ProductName = productName };

        return await _client.GetDiscountAsync(discountRequest);
    }

    #endregion


}
