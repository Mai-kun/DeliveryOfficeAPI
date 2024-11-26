namespace AutoService.Core.Models;

public class PartUsed
{
    public int Id { get; set; }
    public string PartName { get; set; } = null!;
    public decimal PartCost { get; set; }

    public Order? Order { get; set; }
}