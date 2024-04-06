using LoanApp;
using LoanApp.ValueObjects;

const string USAGE = $"Usage: dotnet run <principal> <termInMonths> <rate>";

if (args.Length != 3)
{
    Console.WriteLine(USAGE);
    return;
}

try
{
    MortgagePrincipal principal = new(decimal.Parse(args[0]));
    MortgageTerm term = new(int.Parse(args[1]));
    decimal rate = decimal.Parse(args[2]);
    string outputFile = $"output/amortization_schedule_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.csv";

    using TextWriter writer = new StreamWriter(outputFile);
    CsvAmortizationScheduleWriter amortizationScheduleWriter = new(writer);
    amortizationScheduleWriter.WriteAmortizationSchedule(principal, term, rate);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    Console.WriteLine(USAGE);
}
