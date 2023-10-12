using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TollFee.Api.Models.Reponses;
using TollFee.Api.Models.Requests;
using FluentValidation;
using TollFee.Application.Interfaces;
using System.Threading.Tasks;
using ToolFee.Domain.Extensions;

namespace TollFee.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FeeController : ControllerBase
{
    private readonly IFeeService _feeService;
    private readonly IValidator<GetFeeRequestModel> _getFeeValidator;

    public FeeController(IFeeService feeService, IValidator<GetFeeRequestModel> getFeeValidator)
    {
        _getFeeValidator = getFeeValidator ?? throw new ArgumentNullException(nameof(getFeeValidator));
        _feeService = feeService ?? throw new ArgumentNullException(nameof(feeService));
    }

    [HttpGet]
    public async Task<ActionResult<IList<CalculateFeeResponse>>> CalculateFee([FromQuery] GetFeeRequestModel request)
    {
        //TODO: add mapper from model
        var validationResult = await _getFeeValidator.ValidateAsync(request);
        if (validationResult.IsValid)
        {
            var datesAsDateTime = request.Dates.ToDateTimeEnumerable("yyyy/MM/dd HH:mm:ss");
            var uniqYears = datesAsDateTime.Select(x => x.Year).Distinct();
            var responses = new List<CalculateFeeResponse>();
            foreach (var year in uniqYears)
            {
                //TODO: add mapping request to Dictionary , where key is Year and value List<month>
                var totalFee = _feeService.GetFeeForPassages(datesAsDateTime.Where(x => x.Year == year), year);

                //TODO: add mapper from service response to CalculateFeeResponse
                var response = new CalculateFeeResponse
                {
                    TotalFee = totalFee,
                    AverageFeePerDay = totalFee / request.Dates.Distinct().Count()
                };
                responses.Add(response);
            }

            return responses;
        }
        else
        {
            //TODO: add custom error handling with errors and etc.
            foreach(var error in validationResult.Errors)
            {
                ModelState.AddModelError("Validation error", error.ErrorMessage);
            }
          
            return BadRequest(ModelState);
        }
    }
}
