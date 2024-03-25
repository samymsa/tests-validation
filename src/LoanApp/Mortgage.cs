using LoanApp.InterestRateStrategy;

namespace LoanApp;

public class Mortgage
{
    private readonly decimal Principal;
    private readonly int Term;
    private readonly IInterestRateStrategy RateStrategy;
    public decimal Rate => RateStrategy.Rate;
    public Mortgage(decimal principal, int term, IInterestRateStrategy rateStrategy)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(principal);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(term);

        Principal = principal;
        Term = term;
        RateStrategy = rateStrategy;
    }

    public decimal GetMonthlyPayment(int month)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(month, 1);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(month, Term);

        return 0;
    }
}