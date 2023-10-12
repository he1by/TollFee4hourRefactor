namespace TollFee.Infrastructure.Data;

public class ToolFee
{
    public int Id { get; set; }
    public int Year { get; set; }
    public long FromMinute { get; set; }
    public long ToMinute { get; set; }
    public decimal Price { get; set; }
}
