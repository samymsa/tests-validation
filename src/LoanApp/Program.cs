using LoanApp.AmortizationScheduleWriter;
using LoanApp.InterestRateStrategy;
using LoanApp.Loan;
using LoanApp.LoanParser;
using LoanApp.MonthlyPaymentCalculator;

const string DEFAULT_OUTPUT = "amortization.csv";

MortgageParser parser = new();
int exitCode = parser.ParseArgs(args);
if (exitCode != 0)
{
    return exitCode;
}

Mortgage mortgage = new(parser.Principal, parser.Term, new FixedInterestRate(parser.Rate));
FixedMonthlyPaymentCalculator calculator = new();

try {
    using TextWriter writer = new StreamWriter(parser.Output ?? DEFAULT_OUTPUT);
    CsvAmortizationScheduleWriter amortizationScheduleWriter = new(writer, calculator);
    amortizationScheduleWriter.WriteAmortizationSchedule(mortgage);
}
catch (IOException e)
{
    Console.Error.WriteLine(e.Message);
    return 1;
}

return 0;