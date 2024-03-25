using LoanApp.InterestRateStrategy;

namespace LoanApp;

public class Mortgage
{
    private readonly IInterestRateStrategy RateStrategy;
    public decimal Rate => RateStrategy.Rate;
    public Mortgage(decimal principal, int term, IInterestRateStrategy rateStrategy)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(principal);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(term);

        RateStrategy = rateStrategy;
    }
}