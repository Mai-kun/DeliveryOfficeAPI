namespace AutoService.Core.Models;

public class Car
{
    public int Id { get; set; }
    public string? Model { get; set; }
    public int Year { get; set; }
    public string LicensePlate { get; set; } = null!;
    public string Vin { get; set; } = null!;

    public Customer Customer { get; set; } = null!;
    public List<Order> Orders { get; set; } = null!;
}