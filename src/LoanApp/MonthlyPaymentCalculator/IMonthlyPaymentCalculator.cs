using LoanApp.Loan;

namespace LoanApp.MonthlyPaymentCalculator;

public interface IMonthlyPaymentCalculator
{
    decimal CalculateMonthlyPayment(ILoan loan);
}
