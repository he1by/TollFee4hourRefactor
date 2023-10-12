using FluentValidation.TestHelper;
using TollFee.Api.Models.Requests;
using TollFee.Api.Validators;
using TollFee.Domain.Configuration;

namespace ToolFee.UnitTests.Api.Validators
{
    //TODO: add moq
    public class GetFeeValidatorTests
    {
        private readonly GetFeeValidator validator;

        public GetFeeValidatorTests()
        {
            // Arrange: Create an instance of the validator with mocked AppSettings
            var appSettings = new AppSettings
            {
                Years = new List<FreeDaysForYearConfig>
                {
                    new FreeDaysForYearConfig { Number = 2022 },
                    new FreeDaysForYearConfig { Number = 2023 },
                }
            };
            validator = new GetFeeValidator(appSettings);
        }

        [Fact]
        public void ValidRequestModel_DatesAreValid_ReturnsTrue()
        {
            // Arrange
            var validRequest = new GetFeeRequestModel
            {
                Dates = new List<string> { "2022-01-01", "2023-01-01" }
            };

            // Act
            var result = validator.TestValidate(validRequest);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void InvalidRequestModel_UnconfiguredYear_ReturnsError()
        {
            // Arrange
            var invalidRequest = new GetFeeRequestModel
            {
                Dates = new List<string> { "2024-01-01" }
            };

            // Act
            var result = validator.TestValidate(invalidRequest);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.Dates)
                .WithErrorMessage("Year is not configured.");
        }
    }
}
