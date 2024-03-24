using LoanApp.LoanParser;

MortgageParser parser = new();
int exitCode = parser.ParseArgs(args);
if (exitCode != 0)
{
    return exitCode;
}
return 0;