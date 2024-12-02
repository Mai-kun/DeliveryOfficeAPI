using AutoMapper;
using DeliveryOffice.Core.Models;
using DeliveryOffice.Services.Contracts.Models.ResponseModels;

namespace DeliveryOffice.API.Infrastructure;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Supplier, SupplierDto>()
            .ForMember(dest => dest.Bills, opt => opt.MapFrom(src => src.Bills.Select(b => b.Id)));
    }
}
