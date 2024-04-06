namespace LoanApp.ValueObjects;

public class MortgageTerm
{
    private const int MIN_VALUE = 108;
    private const int MAX_VALUE = 300;
    public int Value { get; }
    public MortgageTerm(int termInMonths)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(termInMonths, MIN_VALUE);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(termInMonths, MAX_VALUE);
        Value = termInMonths;
    }

    public static implicit operator int(MortgageTerm termInMonths) => termInMonths.Value;
    public static implicit operator MortgageTerm(int termInMonths) => new(termInMonths);
}