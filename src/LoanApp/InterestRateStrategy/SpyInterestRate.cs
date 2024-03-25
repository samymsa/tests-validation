namespace LoanApp.InterestRateStrategy;

public class SpyInterestRate : IInterestRateStrategy
{
    public bool RateCalled { get; private set; }

    public decimal Rate
    {
        get
        {
            RateCalled = true;
            return 0;
        }
    }
}