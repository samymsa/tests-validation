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
            _ = new Mortgage(principal, 108, 3.5m);
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
            _ = new Mortgage(100000, term, 3.5m);
        }

        var exception = Assert.Throws<ArgumentOutOfRangeException>(act);
        Assert.Equal("term", exception.ParamName);
    }

    [Fact]
    public void Mortgage_ValidArguments_SetsProperties()
    {
        Mortgage mortgage = new(100000, 108, 3.5m);
        Assert.Equal(100000, mortgage.Principal);
        Assert.Equal(108, mortgage.Term);
    }
}
