using LoanApp.AmortizationScheduleWriter;
using LoanApp.InterestRateStrategy;
using LoanApp.Loan;
using LoanApp.LoanParser;
using LoanApp.MonthlyPaymentCalculator;

const string OUTPUT_FILE = "amortization.csv";

MortgageParser parser = new();
int exitCode = parser.ParseArgs(args);
if (exitCode != 0)
{
    return exitCode;
}

Mortgage mortgage = new(parser.Principal, parser.Term, new FixedInterestRate(parser.Rate));
FixedMonthlyPaymentCalculator calculator = new();
using (TextWriter writer = new StreamWriter(OUTPUT_FILE))
{
    CsvAmortizationScheduleWriter amortizationScheduleWriter = new(writer, calculator);
    amortizationScheduleWriter.WriteAmortizationSchedule(mortgage);
}

return 0;