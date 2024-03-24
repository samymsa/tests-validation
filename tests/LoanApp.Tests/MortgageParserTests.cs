namespace LoanApp.Tests;

using LoanApp.LoanParser;

public class MortgageParserTests
{
    [Theory]
    [InlineData("--principal", "50000", "--term", "108", "--rate", "3.5")]
    [InlineData("--term", "108", "--rate", "3.5", "--principal", "50000")]
    [InlineData("--rate", "3.5", "--principal", "50000", "--term", "108")]
    public void ParseArgs_ValidArgs_ReturnsZero(params string[] args)
    {
        MortgageParser parser = new();
        int exitCode = parser.ParseArgs(args);
        Assert.Equal(0, exitCode);
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
    public void ParseArgs_BadArgs_ReturnsNonZero(params string[] args)
    {
        MortgageParser parser = new();
        int exitCode = parser.ParseArgs(args);
        Assert.NotEqual(0, exitCode);
    }

    [Theory]
    [InlineData("--principal", "50000", "--term", "108")]
    [InlineData("--principal", "50000", "--rate", "3.5")]
    [InlineData("--term", "108", "--rate", "3.5")]
    [InlineData("--principal", "50000")]
    [InlineData("--term", "108")]
    [InlineData("--rate", "3.5")]
    [InlineData()]
    public void ParseArgs_MissingArgs_ReturnsNonZero(params string[] args)
    {
        MortgageParser parser = new();
        int exitCode = parser.ParseArgs(args);
        Assert.NotEqual(0, exitCode);
    }

    [Theory]
    [InlineData("--principal", "50000", "--term", "108", "--rate", "3.5", "--extra")]
    [InlineData("--principal", "50000", "--term", "108", "--rate", "3.5", "--extra", "arg")]
    public void PareArgs_ExtraArgs_ReturnsNonZero(params string[] args)
    {
        MortgageParser parser = new();
        int exitCode = parser.ParseArgs(args);
        Assert.NotEqual(0, exitCode);
    }

    [Fact]
    public void ParseArgs_HelpOption_ReturnsZero()
    {
        MortgageParser parser = new();
        int exitCode = parser.ParseArgs(["--help"]);
        Assert.Equal(0, exitCode);
    }
}