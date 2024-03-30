namespace LoanApp.Tests;

public class MortgageParserTests : IDisposable
{
    private readonly StringWriter _output = new();
    private readonly StringWriter _error = new();
    private readonly TextWriter _originalOut = Console.Out;
    private readonly TextWriter _originalError = Console.Error;

    public MortgageParserTests()
    {
        Console.SetOut(_output);
        Console.SetError(_error);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        Console.SetOut(_originalOut);
        Console.SetError(_originalError);
    }
    
    [Theory]
    [InlineData("--principal", "50000", "--term", "108", "--rate", "3.5")]
    [InlineData("--term", "108", "--rate", "3.5", "--principal", "50000")]
    [InlineData("--rate", "3.5", "--principal", "50000", "--term", "108")]
    [InlineData("--principal", "50000", "--term", "108", "--rate", "3.5", "--output", "output.txt")]
    public void ParseArgs_ValidArgs_Success(params string[] args)
    {
        MortgageParser parser = new();
        int exitCode = parser.ParseArgs(args);
        Assert.Equal(0, exitCode);
        Assert.Empty(_output.ToString());
        Assert.Empty(_error.ToString());
    }

    [Theory]
    [InlineData("--principal", "50000", "--term", "108", "--rate", "3.5")]
    [InlineData("--term", "108", "--rate", "3.5", "--principal", "50000")]
    [InlineData("--rate", "3.5", "--principal", "50000", "--term", "108")]
    public void ParseArgs_ValidArgs_ArgsSet(params string[] args)
    {
        MortgageParser parser = new();
        parser.ParseArgs(args);
        Assert.Equal(50000, parser.Principal);
        Assert.Equal(108, parser.Term);
        Assert.Equal(3.5m, parser.Rate);
    }

    [Theory]
    [InlineData("--principal", "abc", "--term", "108", "--rate", "3.5")]
    [InlineData("--principal", "50000", "--term", "abc", "--rate", "3.5")]
    [InlineData("--principal", "50000", "--term", "108", "--rate", "abc")]
    [InlineData("--principal", "-50000", "--term", "108", "--rate", "3.5")]
    [InlineData("--principal", "50000", "--term", "-108", "--rate", "3.5")]
    [InlineData("--principal", "40000", "--term", "108", "--rate", "3.5")]
    [InlineData("--principal", "50000", "--term", "90", "--rate", "3.5")]
    [InlineData("--principal", "50000", "--term", "301", "--rate", "3.5")]
    public void ParseArgs_BadArgs_Fails(params string[] args)
    {
        MortgageParser parser = new();
        int exitCode = parser.ParseArgs(args);
        Assert.NotEqual(0, exitCode);
        Assert.Contains("Usage", _output.ToString());
        Assert.NotEmpty(_error.ToString());
    }

    [Theory]
    [InlineData("--principal", "50000", "--term", "108")]
    [InlineData("--principal", "50000", "--rate", "3.5")]
    [InlineData("--term", "108", "--rate", "3.5")]
    [InlineData("--principal", "50000")]
    [InlineData("--term", "108")]
    [InlineData("--rate", "3.5")]
    [InlineData("")]
    public void ParseArgs_MissingArgs_Fails(params string[] args)
    {
        MortgageParser parser = new();
        int exitCode = parser.ParseArgs(args);
        Assert.NotEqual(0, exitCode);
        Assert.Contains("Usage", _output.ToString());
        Assert.Contains("is required", _error.ToString());
    }

    [Theory]
    [InlineData("--principal", "50000", "--term", "108", "--rate", "3.5", "--extra")]
    [InlineData("--principal", "50000", "--term", "108", "--rate", "3.5", "--extra", "arg")]
    public void PareArgs_ExtraArgs_Fails(params string[] args)
    {
        MortgageParser parser = new();
        int exitCode = parser.ParseArgs(args);
        Assert.NotEqual(0, exitCode);
        Assert.Contains("Usage", _output.ToString());
        Assert.Contains("Unrecognized command or argument", _error.ToString());
    }

    [Fact]
    public void ParseArgs_HelpOption_Success()
    {
        MortgageParser parser = new();
        int exitCode = parser.ParseArgs(["--help"]);
        Assert.Equal(0, exitCode);
        Assert.Contains("Usage", _output.ToString());
        Assert.Empty(_error.ToString());
    }

    [Fact]
    public void ParseArgs_OutputOption_OutputSet()
    {
        MortgageParser parser = new();
        parser.ParseArgs(["--principal", "50000", "--term", "108", "--rate", "3.5", "--output", "output.txt"]);
        Assert.Equal("output.txt", parser.Output);
    }

    [Fact]
    public void ParseArgs_NoOutputOption_OutputSetToNull()
    {
        MortgageParser parser = new();
        parser.ParseArgs(["--principal", "50000", "--term", "108", "--rate", "3.5"]);
        Assert.Null(parser.Output);
    }
}