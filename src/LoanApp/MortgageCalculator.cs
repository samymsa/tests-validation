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
}