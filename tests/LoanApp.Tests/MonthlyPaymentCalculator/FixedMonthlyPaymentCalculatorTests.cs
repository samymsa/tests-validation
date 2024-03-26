namespace LoanApp.Tests.MonthlyPaymentCalculator;

using LoanApp.InterestRateStrategy;
using LoanApp.Loan;
using LoanApp.MonthlyPaymentCalculator;

public class FixedMonthlyPaymentCalculatorTests
{
    public static IEnumerable<object[]> Data =>
    [
        [new Mortgage(200000, 15 * 12, new FixedInterestRate(3.9m)), 1469.37m],
        [new Mortgage(200000, 15 * 12, new FixedInterestRate(-3.9m)), 815.86m],
        [new Mortgage(200000, 15 * 12, new FixedInterestRate(0.0m)), 1111.11m],
    ];

    [Theory]
    [MemberData(nameof(Data))]
    public void CalculateMonthlyPayment_ReturnsExpectedValue(ILoan loan, decimal expected)
    {
        FixedMonthlyPaymentCalculator calculator = new();
        decimal actual = calculator.CalculateMonthlyPayment(loan);
        Assert.Equal(expected, actual, 2);
    }
}
