using LoanApp.ValueObjects;

namespace LoanApp.Tests;

public class MortgageCalculatorTests
{
    public static IEnumerable<object[]> MonthlyPaymentData =>
        [
            [new MortgagePrincipal(200000), new MortgageTerm(15 * 12), 3.9m, 1469.37m],
            [new MortgagePrincipal(100000), new MortgageTerm(10 * 12), 4.5m, 1036.38m],
            [new MortgagePrincipal(300000), new MortgageTerm(25 * 12), 3.0m, 1422.63m]
        ];

    public static IEnumerable<object[]> TotalCostData =>
        [
            [new MortgagePrincipal(200000), new MortgageTerm(15 * 12), 3.9m, 264487.21],
            [new MortgagePrincipal(100000), new MortgageTerm(10 * 12), 4.5m, 124366.09],
            [new MortgagePrincipal(300000), new MortgageTerm(25 * 12), 3.0m, 426790.18]
        ];

    public static IEnumerable<object[]> AmortizationScheduleData =>
        [
            [new MortgagePrincipal(200000), new MortgageTerm(15 * 12), 3.9m, new List<(int, decimal, decimal)>
                {
                    (1, 819.37m, 199180.63m),
                    (2, 822.04m, 198358.59m),
                    (3, 824.71m, 197533.88m)
                }
            ],
            [new MortgagePrincipal(100000), new MortgageTerm(10 * 12), 4.5m, new List<(int, decimal, decimal)>
                {
                    (1, 661.38m, 99338.62m),
                    (2, 663.86m, 98674.75m),
                    (3, 666.35m, 98008.40m)
                }
            ],
            [new MortgagePrincipal(300000), new MortgageTerm(25 * 12), 3.0m, new List<(int, decimal, decimal)>
                {
                    (1, 672.63m, 299327.37m),
                    (2, 674.32m, 298653.05m),
                    (3, 676.00m, 297977.05m)
                }
            ]
        ];

    [Theory]
    [MemberData(nameof(MonthlyPaymentData))]
    public void CalculateMonthlyPayment_ReturnsExpectedValue(MortgagePrincipal principal, MortgageTerm term, decimal rate, decimal expected)
    {
        decimal actual = MortgageCalculator.CalculateMonthlyPayment(principal, term, rate);
        Assert.Equal(expected, actual, 2);
    }

    [Theory]
    [MemberData(nameof(TotalCostData))]
    public void CalculateTotalCost_ReturnsExpectedValue(MortgagePrincipal principal, MortgageTerm term, decimal rate, decimal expected)
    {
        decimal actual = MortgageCalculator.CalculateTotalCost(principal, term, rate);
        Assert.Equal(expected, actual, 2);
    }

    [Theory]
    [MemberData(nameof(AmortizationScheduleData))]
    public void CalculateAmortizationSchedule_ReturnsExpectedValue(MortgagePrincipal principal, MortgageTerm term, decimal rate, List<(int, decimal, decimal)> expected)
    {
        var actual = MortgageCalculator.CalculateAmortizationSchedule(principal, term, rate);
        var actualFirstThree = actual.Take(3);

        Assert.Equal<int>(term, actual.Count());
        Assert.Equal(expected, actualFirstThree);
    }
}
