namespace LoanApp;

public class Program
{
    const string DEFAULT_OUTPUT = "amortization.csv";

    public static int Main(string[] args)
    {
        MortgageParser parser = new();
        int exitCode = parser.ParseArgs(args);
        if (exitCode != 0)
        {
            return exitCode;
        }

        Mortgage mortgage = new(parser.Principal, parser.Term, parser.Rate);
        return WriteAmortizationSchedule(mortgage, parser.Output);
    }

    private static int WriteAmortizationSchedule(Mortgage mortgage, string? output)
    {
        try
        {
            using TextWriter writer = new StreamWriter(output ?? DEFAULT_OUTPUT);
            MonthlyPaymentCalculator calculator = new();
            CsvAmortizationScheduleWriter amortizationScheduleWriter = new(writer, calculator);
            amortizationScheduleWriter.WriteAmortizationSchedule(mortgage);
        }
        catch (IOException e)
        {
            Console.Error.WriteLine(e.Message);
            return 1;
        }
        return 0;
    }
}
