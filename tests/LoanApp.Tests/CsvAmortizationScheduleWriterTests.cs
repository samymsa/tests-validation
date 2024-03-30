using System.Text;

namespace LoanApp.Tests;

public class CsvAmortizationScheduleWriterTests
{
    public static IEnumerable<object[]> Data =>
    [
        [new Mortgage(400000, 4, 0m), @"Month,Cumulative Payment,Balance
1,100000.00,300000.00
2,200000.00,200000.00
3,300000.00,100000.00
4,400000.00,0.00
"],
        [new Mortgage(240000, 12, 2.5m), @"Month,Cumulative Payment,Balance
1,20271.87,222990.53
2,40543.73,202718.67
3,60815.60,182446.80
4,81087.47,162174.93
5,101359.33,141903.07
6,121631.20,121631.20
7,141903.07,101359.33
8,162174.93,81087.47
9,182446.80,60815.60
10,202718.67,40543.73
11,222990.53,20271.87
12,243262.40,0.00
"],
    ];

    [Theory]
    [MemberData(nameof(Data))]
    public void WriteAmortizationSchedule_WritesExpectedOutput(Mortgage mortgage, string expected)
    {
        StringBuilder sb = new();
        StringWriter sw = new(sb);
        MonthlyPaymentCalculator calculator = new();
        CsvAmortizationScheduleWriter writer = new(sw, calculator);
        writer.WriteAmortizationSchedule(mortgage);

        string actual = sb.ToString();
        Assert.Equal(expected, actual);
    }
}
