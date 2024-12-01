namespace DeliveryOffice.Services.Contracts.Models.RequestModels;

public record CreateSupplierRequest(
    string Name,
    string Address
);
