using LoanApp.ValueObjects;

namespace LoanApp;

public class CsvAmortizationScheduleWriter(TextWriter writer)
{
    private TextWriter Writer { get; } = writer;

    public void WriteAmortizationSchedule(MortgagePrincipal principal, MortgageTerm term, decimal rate)
    {
        decimal totalCost = MortgageCalculator.CalculateTotalCost(principal, term, rate);
        Writer.WriteLine($"Total cost,{totalCost:F2}");
        Writer.WriteLine("Month,Principal,Balance");
        foreach (var (month, principalPaid, remainingPrincipal) in MortgageCalculator.CalculateAmortizationSchedule(principal, term, rate))
        {
            Writer.WriteLine($"{month},{principalPaid:F2},{remainingPrincipal:F2}");
        }
    }
}
