namespace LoanApp;

public class Mortgage
{
    public decimal Principal { get; }
    public int Term { get; }
    public decimal Rate { get; }
    public Mortgage(decimal principal, int term, decimal rate)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(principal);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(term);

        Principal = principal;
        Term = term;
        Rate = rate;
    }

    public decimal GetMonthlyPayment(int month)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(month, 1);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(month, Term);

        return 0;
    }
}