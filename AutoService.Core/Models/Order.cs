namespace AutoService.Core.Models;

public class Order
{
    public int Id { get; set; }
    public decimal TotalWorkCost { get; set; }
    public decimal TotalPartsCost { get; set; }
    public decimal TotalCost { get; set; }
    public int WarrantyDays { get; set; }
    public DateTime OrderDate { get; set; }
    public string? Recommendations { get; set; }

    public Car Car { get; set; } = null!;
    public List<WorkPerformed> Works { get; set; } = null!;
    public List<PartUsed> Parts { get; set; } = null!;
}