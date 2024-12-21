using AutoMapper;
using DeliveryOffice.Core.Models;
using DeliveryOffice.Services.Abstractions.Models.RequestModels;
using DeliveryOffice.Services.Abstractions.Models.ResponseModels;

namespace DeliveryOffice.API.Infrastructure;

/// <inheritdoc />
public class AutoMapperResponseProfile : Profile
{
    public AutoMapperResponseProfile()
    {
        CreateMap<Supplier, SupplierResponse>()
            .ForMember(dest => dest.Bills, opt => opt.MapFrom(src => src.Bills.Select(b => b.Id)));

        CreateMap<Product, ProductResponse>();

        CreateMap<Buyer, BuyerResponse>()
            .ForMember(dest => dest.Bills, opt => opt.MapFrom(src => src.Bills.Select(b => b.Id)));
    }
}
