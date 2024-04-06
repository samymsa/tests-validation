namespace LoanApp.Tests.ValueObjects;

using LoanApp.ValueObjects;

public class MortgagePrincipalTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(49999.99)]
    public void MortgagePrincipal_InvalidPrincipal_ThrowsArgumentOutOfRangeException(decimal value)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new MortgagePrincipal(value));
    }

    [Theory]
    [InlineData(50000)]
    [InlineData(50000.01)]
    public void MortgagePrincipal_ValidPrincipal_SetsValue(decimal value)
    {
        MortgagePrincipal principal = new(value);
        Assert.Equal(value, principal.Value);
    }
}

