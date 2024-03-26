namespace LoanApp.LoanParser;

using System.CommandLine;

public class MortgageParser
{
    private const decimal MINIMUM_PRINCIPAL = 50000;
    private const decimal MINIMUM_TERM_MONTHS = 108;
    private const decimal MAXIMUM_TERM_MONTHS = 300;
    private readonly Option<decimal> PrincipalOption = new(
        aliases: ["--principal", "-p"],
        description: "The principal amount of the loan.")
    {
        IsRequired = true
    };

    private readonly Option<int> TermOption = new(
        aliases: ["--term", "-t"],
        description: "The term of the loan in months.")
    {
        IsRequired = true
    };

    private readonly Option<decimal> RateOption = new(
        aliases: ["--rate", "-r"],
        description: "The annual interest rate of the loan.")
    {
        IsRequired = true
    };

    private readonly RootCommand RootCommand = new("LoanApp");

    public decimal Principal { get; private set; }
    public int Term { get; private set; }
    public decimal Rate { get; private set; }

    public MortgageParser()
    {
        AddValidators();
        AddOptions();
    }

    private void AddValidators()
    {
        AddPrincipalValidators();
        AddTermValidators();
    }

    private void AddPrincipalValidators()
    {
        PrincipalOption.AddValidator(result =>
        {
            try
            {
                if (result.GetValueForOption(PrincipalOption) < MINIMUM_PRINCIPAL)
                    result.ErrorMessage = $"Principal must be at least {MINIMUM_PRINCIPAL:F2} €.";
            }
            catch (InvalidOperationException) { }
        });
    }

    private void AddTermValidators()
    {
        TermOption.AddValidator(result =>
        {
            try
            {
                if (result.GetValueForOption(TermOption) < MINIMUM_TERM_MONTHS)
                    result.ErrorMessage = $"Term must be at least {MINIMUM_TERM_MONTHS} months.";
            }
            catch (InvalidOperationException) { }
        });

        TermOption.AddValidator(result =>
        {
            try
            {
                if (result.GetValueForOption(TermOption) > MAXIMUM_TERM_MONTHS)
                    result.ErrorMessage = $"Term must be at most {MAXIMUM_TERM_MONTHS} months.";
            }
            catch (InvalidOperationException) { }
        });
    }

    private void AddOptions()
    {
        RootCommand.AddOption(PrincipalOption);
        RootCommand.AddOption(TermOption);
        RootCommand.AddOption(RateOption);
    }

    public int ParseArgs(string[] args)
    {
        RootCommand.SetHandler((decimal principal, int term, decimal rate) =>
        {
            Principal = principal;
            Term = term;
            Rate = rate;
        }, PrincipalOption, TermOption, RateOption);
        return RootCommand.Invoke(args);
    }
}
