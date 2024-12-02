namespace DeliveryOffice.Services.Contracts.Models.ResponseModels;

public class SupplierDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public List<Guid> Bills { get; set; } = [];
}
