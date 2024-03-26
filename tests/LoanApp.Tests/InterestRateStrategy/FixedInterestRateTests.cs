namespace LoanApp.Tests.InterestRateStrategy;

using LoanApp.InterestRateStrategy;

public class FixedInterestRateTests
{
    [Fact]
    public void Rate_DecimalValue_ReturnsDecimalValue()
    {
        FixedInterestRate rate = new(3.5m);
        Assert.Equal(3.5m, rate.Rate);
    }
}