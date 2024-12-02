namespace DeliveryOffice.Services.Contracts.Models.ResponseModels;

public class BillDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public string Warehouse { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsPaid { get; set; }
    public BuyerDto Buyer { get; set; }
    public SupplierDto Supplier { get; set; }
    public List<BillProductDto> Products { get; set; }
    public DateTime CreatedAt { get; set; }
}
