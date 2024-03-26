namespace LoanApp.InterestRateStrategy;

public class FixedInterestRate(decimal value) : IInterestRateStrategy
{
    public decimal Rate { get; } = value;
}