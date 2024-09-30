using AutoMapper;

using CouponApi.Models;

namespace CouponApi
{
    public class MappingConfig
    {
        public static MapperConfiguration Initialize()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<Coupon, CouponDto>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
