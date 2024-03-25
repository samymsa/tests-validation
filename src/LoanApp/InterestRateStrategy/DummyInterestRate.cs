namespace LoanApp.InterestRateStrategy;

public class DummyInterestRate : IInterestRateStrategy
{
    public decimal Rate { get; } = 0;
}