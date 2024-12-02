namespace DeliveryOffice.Services.Contracts.Models.ResponseModels;

public class BillProductDto
{
    public Guid BillId { get; set; }
    public Guid ProductId { get; set; }
    public ProductDto Product { get; set; }
}
