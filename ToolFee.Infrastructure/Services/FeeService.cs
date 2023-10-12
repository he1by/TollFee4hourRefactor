using TollFee.Application.Interfaces;
using TollFee.Domain.Configuration;
using TollFee.Domain.Entites.Year;

namespace TollFee.Infrastructure.Services;

public class FeeService : IFeeService
{
    //TODO: move in configuration
    private const int MaxFee = 60;
    private readonly IList<FreeDaysForYearConfig> _freeDaysForYearConfigs;

    public FeeService(AppSettings appSettings)
    {
        _freeDaysForYearConfigs = appSettings.Years;
    }

    public int GetFeeForPassages(IEnumerable<DateTime> passages, int year)
    {
        if(passages == null || !passages.Any()) return default;

        //TODO: add dateformat and move it configuration
        var passagesForFee = RemoveFreeFeeDays(passages, _freeDaysForYearConfigs.FirstOrDefault(x => x.Number == year).FreeMonths);
        var totalFee = default(int);

        foreach (var passage in passages)
        {
            int fee = CalculateFeeForPassageTime(passage.TimeOfDay);
            totalFee += fee;
        }

        return Math.Min(totalFee, MaxFee);
    }

    //TODO: collect rules in DB, and make SQL call for getting range and fees, then compare and remove this code. And we need to remove all 'magic' numbers
    private int CalculateFeeForPassageTime(TimeSpan passageTime)
    {
        if (IsInTimeRange(passageTime, 6 * 60, 6 * 60 + 29))
            return 9;
        if (IsInTimeRange(passageTime, 6 * 60 + 30, 6 * 60 + 59))
            return 16;
        if (IsInTimeRange(passageTime, 7 * 60, 7 * 60 + 59))
            return 22;
        if (IsInTimeRange(passageTime, 8 * 60, 8 * 60 + 29))
            return 16;
        if (IsInTimeRange(passageTime, 8 * 60 + 30, 14 * 60 + 59))
            return 9;
        if (IsInTimeRange(passageTime, 15 * 60, 15 * 60 + 29))
            return 16;
        if (IsInTimeRange(passageTime, 15 * 60 + 30, 16 * 60 + 59))
            return 22;
        if (IsInTimeRange(passageTime, 17 * 60, 17 * 60 + 59))
            return 16;
        if (IsInTimeRange(passageTime, 18 * 60, 18 * 60 + 29))
            return 9;

        return 0;
    }

    //TODO: move all methods for working with date in helper
    private bool IsInTimeRange(TimeSpan time, int startMinutes, int endMinutes)
    {
        return time.TotalMinutes >= startMinutes && time.TotalMinutes <= endMinutes;
    }

    private IEnumerable<DateTime> RemoveFreeFeeDays(IEnumerable<DateTime> passages, IList<FreeMonth> freeMonths)
    {
        foreach (var passage in passages)
        {
            var freeMonth = freeMonths.FirstOrDefault(x => x.Month == passage.Month);
            if (!IsMonthFree(passage) && !IsWeekend(passage) && !IsFreeDay(passage, freeMonth))
            {
                yield return passage;
            }
        }
    }

    private bool IsWeekend(DateTime passage)
    {
        return passage.DayOfWeek == DayOfWeek.Saturday || passage.DayOfWeek == DayOfWeek.Sunday;
    }

    private bool IsFreeDay(DateTime passage, FreeMonth freeMonth)
    {
        var day = passage.Day;
        return freeMonth != null && freeMonth.Days.Contains(day);
    }

    private bool IsMonthFree(DateTime passage)
    {
        return _freeDaysForYearConfigs.Any(x => x.Number == passage.Year && x.FreeMonths.Any(x => x.Month == passage.Month && x.IsFullFree));
    }
}
