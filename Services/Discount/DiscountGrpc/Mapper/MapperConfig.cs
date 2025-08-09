
using AutoMapper;
using DiscountGrpc.Entities;
using DiscountGrpc.Protos;

namespace DiscountGrpc.Mapper;

public class MapperConfig : Profile
{


    public MapperConfig()
    {


        CreateMap<Coupon, CouponModel>().ReverseMap();
    }



}
