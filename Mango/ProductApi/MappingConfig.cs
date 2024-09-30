using AutoMapper;

using ProductApi.Dto;
using ProductApi.Models;

namespace ProductApi
{
    public class MappingConfig
    {
        public static MapperConfiguration Initialize()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<Product, ProductDto>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
