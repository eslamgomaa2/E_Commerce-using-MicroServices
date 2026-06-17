using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Specs;

namespace Catalog.Application.Mappers
{
    public class ProductMapperProfile : Profile
    {
        public ProductMapperProfile()
        {
            CreateMap<Product, ProductResponseDto>().ReverseMap();
            CreateMap<ProductBrand, BrandsResponseDto>().ReverseMap();
            CreateMap<ProductType, TypesResponseDto>().ReverseMap();
            CreateMap<CreateProductCommand, Product>().ReverseMap();
            CreateMap<Pagination<ProductResponseDto>, Pagination<Product>>().ReverseMap();

        }
    }
}
