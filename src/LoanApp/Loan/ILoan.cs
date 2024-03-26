using LoanApp.InterestRateStrategy;

namespace LoanApp.Loan;

public interface ILoan
{
    decimal Principal { get; }
    int Term { get; }
    decimal Rate { get; }
}
