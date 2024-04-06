using LoanApp;

const string USAGE = $"Usage: dotnet run <principal> <termInMonths> <rate>";

try
{
    MortgageParser parser = new(args);
    string outputFile = $"out/amortization_schedule_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.csv";
    using TextWriter writer = new StreamWriter(outputFile);
    CsvAmortizationScheduleWriter amortizationScheduleWriter = new(writer);
    amortizationScheduleWriter.WriteAmortizationSchedule(parser.Principal, parser.Term, parser.Rate);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    Console.WriteLine(USAGE);
}
