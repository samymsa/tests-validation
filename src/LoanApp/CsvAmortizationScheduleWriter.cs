namespace LoanApp;

public class CsvAmortizationScheduleWriter(TextWriter writer, MonthlyPaymentCalculator calculator)
{
    private TextWriter Writer { get; } = writer;
    private MonthlyPaymentCalculator Calculator { get; } = calculator;

    public void WriteAmortizationSchedule(Mortgage mortgage)
    {
        decimal monthlyPayment = Calculator.CalculateMonthlyPayment(mortgage);
        decimal balance = monthlyPayment * mortgage.Term;
        decimal cumulativePayment = 0;
        Writer.WriteLine("Month,Cumulative Payment,Balance");
        for (int i = 1; i <= mortgage.Term; i++)
        {
            balance -= monthlyPayment;
            cumulativePayment += monthlyPayment;
            decimal roundedBalance = Math.Round(balance, 2);
            decimal roundedCumulativePayment = Math.Round(cumulativePayment, 2);
            Writer.WriteLine($"{i},{roundedCumulativePayment:F2},{roundedBalance:F2}");
        }
    }
}
