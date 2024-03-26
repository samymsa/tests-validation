using LoanApp.InterestRateStrategy;

namespace LoanApp.Loan;

public class Mortgage: ILoan
{
    public decimal Principal { get; }
    public int Term { get; }
    private IInterestRateStrategy RateStrategy { get; }
    public decimal Rate => RateStrategy.Rate;
    public Mortgage(decimal principal, int term, IInterestRateStrategy rateStrategy)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(principal);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(term);

        RateStrategy = rateStrategy;
    }
}