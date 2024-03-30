namespace LoanApp.Tests;

public class FixedMonthlyPaymentCalculatorTests
{
    public static IEnumerable<object[]> Data =>
    [
        [new Mortgage(200000, 15 * 12, 3.9m), 1469.37m],
        [new Mortgage(200000, 15 * 12, -3.9m), 815.86m],
        [new Mortgage(200000, 15 * 12, 0.0m), 1111.11m],
    ];

    [Theory]
    [MemberData(nameof(Data))]
    public void CalculateMonthlyPayment_ReturnsExpectedValue(Mortgage mortgage, decimal expected)
    {
        MonthlyPaymentCalculator calculator = new();
        decimal actual = calculator.CalculateMonthlyPayment(mortgage);
        Assert.Equal(expected, actual, 2);
    }
}
