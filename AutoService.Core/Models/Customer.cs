namespace AutoService.Core.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Phone { get; set; }
    public List<Car> Cars { get; set; } = null!;
}