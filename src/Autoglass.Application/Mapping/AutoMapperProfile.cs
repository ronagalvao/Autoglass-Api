using Autoglass.Application.Dtos;
using Autoglass.Domain.Entities;
using AutoMapper;

namespace Autoglass.Application.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Supplier, SupplierDto>().ReverseMap();
    }
}
