using DeliveryOffice.Core.Models;

namespace DeliveryOffice.Services.Contracts.Models.ResponseModels;

public record SupplierResponse(
    Guid Id,
    string Name,
    string Address
);
