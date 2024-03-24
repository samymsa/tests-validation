namespace LoanApp.Tests;

public class MortgageTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Mortgage_InvalidPrincipal_ThrowsArgumentOutOfRangeException(decimal principal)
    {
        void act()
        {
            _ = new Mortgage(principal, 108, 0.05m);
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
            _ = new Mortgage(100000, term, 0.05m);
        }

        var exception = Assert.Throws<ArgumentOutOfRangeException>(act);
        Assert.Equal("term", exception.ParamName);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(109)]
    public void GetMonthlyPayment_InvalidMonth_ThrowsArgumentOutOfRangeException(int month)
    {
        var mortgage = new Mortgage(100000, 108, 0.05m);
        void act()
        {
            _ = mortgage.GetMonthlyPayment(month);
        }

        var exception = Assert.Throws<ArgumentOutOfRangeException>(act);
        Assert.Equal("month", exception.ParamName);
    }
}
