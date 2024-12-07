namespace DeliveryOffice.Core.RequestModels;

public class SupplierRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
}
