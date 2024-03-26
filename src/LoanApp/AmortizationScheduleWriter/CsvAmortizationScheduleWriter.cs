using LoanApp.Loan;
using LoanApp.MonthlyPaymentCalculator;

namespace LoanApp.AmortizationScheduleWriter;

public class CsvAmortizationScheduleWriter
{
    private TextWriter Writer { get; }
    private IMonthlyPaymentCalculator Calculator { get; }
    public CsvAmortizationScheduleWriter(TextWriter writer, IMonthlyPaymentCalculator calculator)
    {
        Writer = writer;
        Calculator = calculator;
    }

    public void WriteAmortizationSchedule(ILoan loan)
    {
        decimal monthlyPayment = Calculator.CalculateMonthlyPayment(loan);
        decimal balance = monthlyPayment * loan.Term;
        decimal cumulativePayment = 0;
        Writer.WriteLine("Month,Cumulative Payment,Balance");
        for (int i = 1; i <= loan.Term; i++)
        {
            balance -= monthlyPayment;
            cumulativePayment += monthlyPayment;
            decimal roundedBalance = Math.Round(balance, 2);
            decimal roundedCumulativePayment = Math.Round(cumulativePayment, 2);
            Writer.WriteLine($"{i},{roundedCumulativePayment:F2},{roundedBalance:F2}");
        }
    }
}
