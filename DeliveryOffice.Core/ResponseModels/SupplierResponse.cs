namespace DeliveryOffice.Core.ResponseModels;

public class SupplierResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public List<Guid> Bills { get; set; }
}
