using LoanApp.Loan;

namespace LoanApp.MonthlyPaymentCalculator;

public class FixedMonthlyPaymentCalculator : IMonthlyPaymentCalculator
{
    public decimal CalculateMonthlyPayment(ILoan loan)
    {
        decimal monthlyRate = loan.Rate / 12 / 100;
        if (monthlyRate == 0)
        {
            return loan.Principal / loan.Term;
        }
        return loan.Principal * monthlyRate / (1 - (decimal)Math.Pow(1 + (double)monthlyRate, -loan.Term));
    }
}
