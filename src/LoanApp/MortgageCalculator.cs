using LoanApp.ValueObjects;

namespace LoanApp;

public class MortgageCalculator
{
    public static decimal CalculateMonthlyPayment(MortgagePrincipal principal, MortgageTerm term, decimal rate)
    {
        decimal monthlyRate = rate / 12 / 100;
        if (monthlyRate == 0)
        {
            return principal / term;
        }
        return principal * monthlyRate / (1 - (decimal)Math.Pow(1 + (double)monthlyRate, -term));
    }

    public static decimal CalculateTotalCost(MortgagePrincipal principal, MortgageTerm term, decimal rate)
    {
        return CalculateMonthlyPayment(principal, term, rate) * term;
    }

    public static IEnumerable<(int month, decimal principalPaid, decimal remainingPrincipal)> CalculateAmortizationSchedule(MortgagePrincipal principal, MortgageTerm term, decimal rate)
    {
        decimal monthlyPayment = CalculateMonthlyPayment(principal, term, rate);
        decimal remainingPrincipal = principal;
        for (int i = 1; i <= term; i++)
        {
            decimal interest = remainingPrincipal * rate / 100 / 12;
            decimal principalPaid = monthlyPayment - interest;
            remainingPrincipal -= principalPaid;
            yield return (i, Math.Round(principalPaid, 2), Math.Round(remainingPrincipal, 2));
        }
    }
}