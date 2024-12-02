namespace DeliveryOffice.Services.Contracts.Models.ResponseModels;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public string Unit { get; set; }
    public decimal Price { get; set; }
}
