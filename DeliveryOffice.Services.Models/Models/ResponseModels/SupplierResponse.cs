using DeliveryOffice.Core.Models;

namespace DeliveryOffice.Services.Models.Models.ResponseModels;

public record SupplierResponse(
    Guid Id,
    string Name,
    string Address,
    List<Bill> Bills
);
