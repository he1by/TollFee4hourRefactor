namespace TollFee.Application.Interfaces;

public interface IFeeService
{
    public int GetFeeForPassages(IEnumerable<DateTime> passages, int year);
}
