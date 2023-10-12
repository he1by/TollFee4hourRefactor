namespace TollFee.Api.Models.Reponses;

public class CalculateFeeResponse
{
    public int TotalFee { get; set; }
    public float AverageFeePerDay { get; set; }
}
