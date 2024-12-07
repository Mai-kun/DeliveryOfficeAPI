using AutoMapper;
using DeliveryOffice.Core.Models;
using DeliveryOffice.Core.RequestModels;
using DeliveryOffice.Core.ResponseModels;

namespace DeliveryOffice.API.Infrastructure;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Supplier, SupplierResponse>()
            .ForMember(dest => dest.Bills, opt => opt.MapFrom(src => src.Bills.Select(b => b.Id)));

        CreateMap<CreateSupplierRequest, Supplier>().ReverseMap();
    }
}
