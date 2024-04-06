using LoanApp.ValueObjects;

namespace LoanApp;

public class MortgageParser
{
    public static readonly int ARGS_COUNT = 3;
    public MortgagePrincipal Principal { get; }
    public MortgageTerm Term { get; }
    public decimal Rate { get; }

    public MortgageParser(string[] args)
    {
        if (args.Length != ARGS_COUNT)
        {
            throw new ArgumentException($"Expected {ARGS_COUNT} arguments, but got {args.Length}");
        }

        Principal = new MortgagePrincipal(decimal.Parse(args[0]));
        Term = new MortgageTerm(int.Parse(args[1]));
        Rate = decimal.Parse(args[2]);
    }
}