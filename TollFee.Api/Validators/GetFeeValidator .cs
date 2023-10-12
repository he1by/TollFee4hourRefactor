using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using TollFee.Domain.Configuration;
using TollFee.Api.Models.Requests;

namespace TollFee.Api.Validators;

public class GetFeeValidator : AbstractValidator<GetFeeRequestModel>
{
    private readonly IList<FreeDaysForYearConfig> _freeDaysForYearConfigs;

    public GetFeeValidator(AppSettings appSettings)
    {
        _freeDaysForYearConfigs = appSettings.Years;

        RuleForEach(model => model.Dates)
            .NotNull()
            .NotEmpty()
            .WithMessage("Dates is required.")
            .DependentRules(() =>
            {
                RuleForEach(model => model.Dates)
               //TODO: refactor part with TryParse
                .Must(model => DateTime.TryParse(model, out var stringAsDateTime) && _freeDaysForYearConfigs.Any(x => x.Number == stringAsDateTime.Year))
                .WithMessage("Year is not configured.");
            });

    }
}

