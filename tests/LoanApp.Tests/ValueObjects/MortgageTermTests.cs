namespace LoanApp.Tests.ValueObjects;

using LoanApp.ValueObjects;

public class MortgageTermTests
{
    [Theory]
    [InlineData(107)]
    [InlineData(301)]
    public void MortgageTerm_InvalidTerm_ThrowsArgumentOutOfRangeException(int value)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new MortgageTerm(value));
    }

    [Theory]
    [InlineData(108)]
    [InlineData(300)]
    public void MortgageTerm_ValidTerm_SetsValue(int value)
    {
        MortgageTerm term = new(value);
        Assert.Equal(value, term.Value);
    }
}