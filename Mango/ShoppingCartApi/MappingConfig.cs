using AutoMapper;

using ShoppingCartApi.Models;
using ShoppingCartApi.Models.Dto;

namespace ShoppingCartApi
{
    public class MappingConfig
    {
        public static MapperConfiguration Initialize()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<Cart, CartDTO>().ReverseMap();
                mc.CreateMap<CartDetail, CartDetailDTO>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
