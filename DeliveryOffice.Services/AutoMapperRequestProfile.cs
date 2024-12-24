using AutoMapper;
using DeliveryOffice.Core.Models;
using DeliveryOffice.Services.Abstractions.Models.RequestModels;

namespace DeliveryOffice.Services;

public class AutoMapperRequestProfile : Profile
{
    public AutoMapperRequestProfile()
    {
        CreateMap<SupplierRequest, Supplier>().ReverseMap();

        CreateMap<ProductRequest, Product>().ReverseMap();

        CreateMap<BuyerRequest, Buyer>().ReverseMap();

        CreateMap<CreateBillRequest, Bill>()
            .ForMember(dest => dest.Products, opt => opt.Ignore());
        CreateMap<BillRequest, Bill>().ReverseMap();
    }
}
