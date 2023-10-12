using TollFee.Domain.Entites.Year;

namespace TollFee.Domain.Configuration;

public class FreeDaysForYearConfig
{
    public int Number { get; set; }
    public List<FreeMonth> FreeMonths { get; set; }
}
