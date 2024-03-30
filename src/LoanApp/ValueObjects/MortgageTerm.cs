namespace LoanApp.ValueObjects;

public class MortgageTerm
{
    private const int MIN_VALUE = 108;
    private const int MAX_VALUE = 300;
    public int Value { get; }
    public MortgageTerm(int term)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(term, MIN_VALUE);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(term, MAX_VALUE);
        Value = term;
    }

    public static implicit operator int(MortgageTerm term) => term.Value;
    public static implicit operator MortgageTerm(int term) => new(term);
}