namespace LoanApp;

public class MonthlyPaymentCalculator
{
    public decimal CalculateMonthlyPayment(Mortgage mortgage)
    {
        decimal monthlyRate = mortgage.Rate / 12 / 100;
        if (monthlyRate == 0)
        {
            return mortgage.Principal / mortgage.Term;
        }
        return mortgage.Principal * monthlyRate / (1 - (decimal)Math.Pow(1 + (double)monthlyRate, -mortgage.Term));
    }
}
