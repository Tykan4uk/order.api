using AutoMapper;
using OrderApi.Data.Entities;
using OrderApi.Models;

namespace OrderApi.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderModel, OrderEntity>().ReverseMap();
            CreateMap<ProductModel, ProductEntity>().ReverseMap();
        }
    }
}
