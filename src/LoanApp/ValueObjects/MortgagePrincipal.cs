namespace LoanApp.ValueObjects;

public class MortgagePrincipal
{
    private const decimal MIN_VALUE = 50000;
    public decimal Value { get; }
    public MortgagePrincipal(decimal principal)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(principal, MIN_VALUE);
        Value = principal;
    }

    public static implicit operator decimal(MortgagePrincipal principal) => principal.Value;
    public static implicit operator MortgagePrincipal(decimal principal) => new(principal);
}