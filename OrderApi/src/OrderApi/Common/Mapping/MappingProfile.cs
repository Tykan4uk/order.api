using AutoMapper;
using OrderApi.Data;
using OrderApi.Data.Entities;
using OrderApi.Models;
using OrderApi.Models.Responses;

namespace OrderApi.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderModel, OrderEntity>().ReverseMap();
            CreateMap<ProductModel, ProductEntity>().ReverseMap();
            CreateMap<GetByPageResponse, PagingDataResult>().ReverseMap();
        }
    }
}
