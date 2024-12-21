using AutoMapper;
using DeliveryOffice.Core.Models;
using DeliveryOffice.Services.Abstractions.Models.RequestModels;

namespace DeliveryOffice.Services;

public class AutoMapperRequestProfile : Profile
{
    public AutoMapperRequestProfile()
    {
        CreateMap<CreateSupplierRequest, Supplier>().ReverseMap();
        CreateMap<SupplierRequest, Supplier>().ReverseMap();

        CreateMap<CreateProductRequest, Product>().ReverseMap();
        CreateMap<ProductRequest, Product>().ReverseMap();

        CreateMap<CreateBuyerRequest, Buyer>().ReverseMap();
        CreateMap<BuyerRequest, Buyer>().ReverseMap();
    }
}
