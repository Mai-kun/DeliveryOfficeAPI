namespace DeliveryOffice.Services.Contracts.Models.ResponseModels;

public class BuyerDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}
