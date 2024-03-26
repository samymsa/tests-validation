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
decimal principal = parser.Principal ?? throw new ArgumentException("Principal must not be null");
int term = parser.Term ?? throw new ArgumentException("Term must not be null");
decimal rate = parser.Rate ?? throw new ArgumentException("Rate must not be null");

Mortgage mortgage = new(principal, term, new FixedInterestRate(rate));
FixedMonthlyPaymentCalculator calculator = new();
using (TextWriter writer = new StreamWriter(OUTPUT_FILE))
{
    CsvAmortizationScheduleWriter amortizationScheduleWriter = new(writer, calculator);
    amortizationScheduleWriter.WriteAmortizationSchedule(mortgage);
}

return 0;