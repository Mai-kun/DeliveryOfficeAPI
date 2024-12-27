using AutoMapper;
using DeliveryOffice.Core.Models;
using DeliveryOffice.Services.Abstractions.RequestModels;

namespace DeliveryOffice.Services;

public class AutoMapperRequestProfile : Profile
{
    public AutoMapperRequestProfile()
    {
        CreateMap<SupplierRequest, Supplier>().ReverseMap();
        CreateMap<CreateSupplierRequest, Supplier>().ReverseMap();

        CreateMap<ProductRequest, Product>().ReverseMap();
        CreateMap<CreateProductRequest, Product>().ReverseMap();

        CreateMap<BuyerRequest, Buyer>().ReverseMap();
        CreateMap<CreateBuyerRequest, Buyer>().ReverseMap();

        CreateMap<CreateBillRequest, Bill>()
            .ForMember(dest => dest.Products, opt => opt.Ignore());
        CreateMap<BillRequest, Bill>().ReverseMap();
    }
}
