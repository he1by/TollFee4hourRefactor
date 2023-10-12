using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace TollFee.Domain.Entites.Year;

public class FreeMonth
{
    [JsonProperty(nameof(Month))]
    public int Month { get; set; }

    [AllowNull]
    [JsonProperty(nameof(Days))]
    public int[] Days { get; set; }

    [JsonProperty(nameof(IsFullFree))]
    public bool IsFullFree { get; set; }
  
}
