using LoanApp.ValueObjects;

namespace LoanApp.Tests;

public class MortgageCalculatorTests
{
    public static IEnumerable<object[]> Data =>
        [
            [new MortgagePrincipal(200000), new MortgageTerm(15 * 12), 3.9m, 1469.37m],
            [new MortgagePrincipal(100000), new MortgageTerm(10 * 12), 4.5m, 1036.38m],
            [new MortgagePrincipal(300000), new MortgageTerm(25 * 12), 3.0m, 1422.63m]
        ];

    [Theory]
    [MemberData(nameof(Data))]
    public void CalculateMonthlyPayment_ReturnsExpectedValue(MortgagePrincipal principal, MortgageTerm term, decimal rate, decimal expected)
    {
        decimal actual = MortgageCalculator.CalculateMonthlyPayment(principal, term, rate);
        Assert.Equal(expected, actual, 2);
    }
}
