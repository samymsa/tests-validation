using LoanApp.InterestRateStrategy;
using LoanApp.Loan;

namespace LoanApp.Tests.Loan;

public class MortgageTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Mortgage_InvalidPrincipal_ThrowsArgumentOutOfRangeException(decimal principal)
    {
        void act()
        {
            _ = new Mortgage(principal, 108, new DummyInterestRate());
        }

        var exception = Assert.Throws<ArgumentOutOfRangeException>(act);
        Assert.Equal("principal", exception.ParamName);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Mortgage_InvalidTerm_ThrowsArgumentOutOfRangeException(int term)
    {
        void act()
        {
            _ = new Mortgage(100000, term, new DummyInterestRate());
        }

        var exception = Assert.Throws<ArgumentOutOfRangeException>(act);
        Assert.Equal("term", exception.ParamName);
    }

    [Fact]
    public void Mortgage_ValidArguments_SetsProperties()
    {
        Mortgage mortgage = new(100000, 108, new DummyInterestRate());
        Assert.Equal(100000, mortgage.Principal);
        Assert.Equal(108, mortgage.Term);
    }

    [Fact]
    public void Rate_FixedInterestRate_ReturnsFixedRate()
    {
        FixedInterestRate rateStrategy = new(3.5m);
        Mortgage mortgage = new(100000, 108, rateStrategy);
        Assert.Equal(rateStrategy.Rate, mortgage.Rate);
    }

    [Fact]
    public void Rate_SpyInterestRate_RateCalled()
    {
        SpyInterestRate rateStrategy = new();
        Mortgage mortgage = new(100000, 108, rateStrategy);
        _ = mortgage.Rate;
        Assert.True(rateStrategy.RateCalled);
    }
}
