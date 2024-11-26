namespace AutoService.Core.Models;

public class WorkPerformed
{
    public int Id { get; set; }
    public string WorkName { get; set; } = null!;
    public decimal WorkCost { get; set; }

    public Order? Order { get; set; }
}