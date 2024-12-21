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

        CreateMap<Bill, BillResponse>()
            .ForMember(dest => dest.ProductNames, opt => opt.MapFrom(src => src.Products.Select(p => p.Name)))
            .ForMember(
                dest => dest.BuyerName,
                opt => opt.MapFrom(src => src.Buyer != null ? src.Buyer.Name : "Покупатель не найден"))
            .ForMember(
                dest => dest.SupplierName,
                opt => opt.MapFrom(src => src.Supplier != null ? src.Supplier.Name : "Поставщик не найден"));
    }
}
