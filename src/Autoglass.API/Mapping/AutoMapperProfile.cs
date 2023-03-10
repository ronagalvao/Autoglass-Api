using Autoglass.Application.Dtos;
using Autoglass.Domain.Entities;
using AutoMapper;

namespace Autoglass.API.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Supplier, SupplierDto>().ReverseMap();
        CreateMap<Product, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
