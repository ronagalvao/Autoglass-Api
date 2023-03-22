using Stockmate.Application.Dtos;
using Stockmate.Domain.Entities;
using AutoMapper;

namespace Stockmate.Api.Mappings;

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
