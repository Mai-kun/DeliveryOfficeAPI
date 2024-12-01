namespace DeliveryOffice.Services.Contracts.Models.RequestModels;

public record UpdateSupplierRequest(
    string? Name,
    string? Address
);
