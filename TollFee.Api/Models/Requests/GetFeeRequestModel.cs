using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TollFee.Api.Models.Requests;

public class GetFeeRequestModel
{
    [Required]
    public IEnumerable<string> Dates { get; set; }
}
