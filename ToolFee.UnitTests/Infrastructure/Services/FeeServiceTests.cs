using TollFee.Domain.Configuration;
using TollFee.Domain.Entites.Year;
using TollFee.Infrastructure.Services;

namespace ToolFee.UnitTests.Infrastructure.Services;

//TODO: add moq
public class FeeServiceTests
{
    [Fact]
    public void GetFeeForPassages_NoPassages_ReturnsZero()
    {
        // Arrange
        var appSettings = new AppSettings
        {
            Years = new List<FreeDaysForYearConfig> { new FreeDaysForYearConfig() }
        };

        var feeService = new FeeService(appSettings);

        // Act
        var result = feeService.GetFeeForPassages(new List<DateTime>(), 2021);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void GetFeeForPassages_SinglePassage_ReturnsCorrectFee()
    {
        // Arrange
        var appSettings = new AppSettings
        {
            Years = new List<FreeDaysForYearConfig> {
                new FreeDaysForYearConfig() {
                    Number = 2021,
                    FreeMonths = new List<FreeMonth> {
                        new FreeMonth() {
                            IsFullFree = true
                        }
                    }
                }
            }
        };

        var feeService = new FeeService(appSettings);

        var passages = new List<DateTime>
        {
            new DateTime(2021, 10, 12, 7, 15, 0)
        };

        // Act
        var result = feeService.GetFeeForPassages(passages, 2021);

        // Assert
        Assert.Equal(22, result);
    }

    [Fact]
    public void GetFeeForPassages_PassagesOnFreeDays_ReturnsZero()
    {
        // Arrange
        var appSettings = new AppSettings
        {
            Years = new List<FreeDaysForYearConfig>
            {
                new FreeDaysForYearConfig
                {
                    Number = 2021,
                    FreeMonths = new List<FreeMonth> { new FreeMonth { Month = 10, IsFullFree = true } }
                }
            }
        };

        var feeService = new FeeService(appSettings);

        var passages = new List<DateTime>
        {
            new DateTime(2021, 10, 11, 8, 25, 0)
        };

        // Act
        var result = feeService.GetFeeForPassages(passages, 2021);

        // Assert
        Assert.Equal(16, result);
    }
}
