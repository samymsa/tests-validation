using System.Text;
using LoanApp.ValueObjects;

namespace LoanApp.Tests;

public class CsvAmortizationScheduleWriterTests
{
    public static IEnumerable<object[]> Data =>
        [
            [new MortgagePrincipal(200000), new MortgageTerm(15 * 12), 3.9m],
            [new MortgagePrincipal(100000), new MortgageTerm(10 * 12), 4.5m],
            [new MortgagePrincipal(300000), new MortgageTerm(25 * 12), 3.0m]
        ];

    [Theory]
    [MemberData(nameof(Data))]
    public void WriteAmortizationSchedule_WritesTotalCost(MortgagePrincipal principal, MortgageTerm term, decimal rate)
    {
        StringBuilder sb = new();
        using TextWriter writer = new StringWriter(sb);
        CsvAmortizationScheduleWriter amortizationScheduleWriter = new(writer);

        amortizationScheduleWriter.WriteAmortizationSchedule(principal, term, rate);

        string[] lines = sb.ToString().Split(Environment.NewLine);
        decimal totalCost = MortgageCalculator.CalculateTotalCost(principal, term, rate);
        Assert.Equal($"Total mortgage cost: {totalCost:F2}", lines[0]);
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void WriteAmortizationSchedule_WritesHeader(MortgagePrincipal principal, MortgageTerm term, decimal rate)
    {
        StringBuilder sb = new();
        using TextWriter writer = new StringWriter(sb);
        CsvAmortizationScheduleWriter amortizationScheduleWriter = new(writer);

        amortizationScheduleWriter.WriteAmortizationSchedule(principal, term, rate);

        string[] lines = sb.ToString().Split(Environment.NewLine);
        Assert.Equal("Month,Principal,Balance", lines[1]);
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void WriteAmortizationSchedule_WritesExpectedSchedule(MortgagePrincipal principal, MortgageTerm term, decimal rate)
    {
        StringBuilder sb = new();
        using TextWriter writer = new StringWriter(sb);
        CsvAmortizationScheduleWriter amortizationScheduleWriter = new(writer);

        amortizationScheduleWriter.WriteAmortizationSchedule(principal, term, rate);

        string[] lines = sb.ToString().Split(Environment.NewLine);
        var schedule = MortgageCalculator.CalculateAmortizationSchedule(principal, term, rate);
        foreach (var (month, principalPaid, remainingPrincipal) in schedule)
        {
            Assert.Equal($"{month},{principalPaid:F2},{remainingPrincipal:F2}", lines[month + 1]);
        }
    }
}
