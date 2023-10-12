using Newtonsoft.Json;

namespace TollFee.Domain.Entites.Year;

public class Year
{
    [JsonProperty(nameof(Number))]
    public long Number { get; set; }

    [JsonProperty(nameof(FreeMonths))]
    public FreeMonth[] FreeMonths { get; set; }
}
