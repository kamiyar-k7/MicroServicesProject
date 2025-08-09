
using AutoMapper;
using DiscountGrpc.Entities;
using DiscountGrpc.Protos;
using DiscountGrpc.Repository;
using Grpc.Core;
using Npgsql;

namespace DiscountGrpc.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    #region ctor

    private readonly IDiscountRepository _repository;
    private readonly ILogger<DiscountService> _logger;
    private readonly IMapper _mapper;
    public DiscountService(IDiscountRepository repository, ILogger<DiscountService> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
    #endregion

    #region Get Discount

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {

      

        var coupon = await _repository.GetDiscount(request.ProductName);
        if (coupon == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"discount with product name {request.ProductName} not found!"));

        }

        _logger.LogInformation("discount is retrieved for product name");

        return _mapper.Map<CouponModel>(coupon);

        //return new CouponModel
        //{
        //    ProductName = coupon.ProductName,
        //    Amount = coupon.Amount,
        //    Description = coupon.Description,
        //    Id = coupon.Id
        //};
   


    }

    #endregion

    #region Create Discount 

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = _mapper.Map<Coupon>(request.Coupon);

        await _repository.CreateDiscount(coupon);

        _logger.LogInformation($" Discount is successfully created for {coupon.ProductName}");

        var cm = _mapper.Map<CouponModel>(coupon);
        return cm;
    }

    #endregion

    #region Update Discount

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = _mapper.Map<Coupon>(request.Coupon);

        await _repository.UpdateDiscount(coupon);

        _logger.LogInformation($"discount is successfully updated for {coupon.ProductName}");

        var cm = _mapper.Map<CouponModel>(coupon);
        return cm;
    }
    #endregion


    #region Delete Discount
    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var deleted = await _repository.DeleteDiscount(request.ProductName);
        return new DeleteDiscountResponse
        {
            Succes = deleted
        };
    }
    #endregion
}
