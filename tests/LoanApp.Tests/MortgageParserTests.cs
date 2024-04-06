namespace LoanApp.Tests;

public class MortgageParserTests
{
    [Theory]
    [InlineData()]
    [InlineData("a")]
    [InlineData("a", "b")]
    [InlineData("a", "b", "c", "d")]
    public void MortgageParser_BadNumberOfArguments_ThrowsArgumentException(params string[] args)
    {
        var ex = Assert.Throws<ArgumentException>(() => new MortgageParser(args));
        Assert.Equal($"Expected {MortgageParser.ARGS_COUNT} arguments, but got {args.Length}", ex.Message);
    }

    [Fact]
    public void MortgageParser_ValidArguments_ParsesArguments()
    {
        string[] args = ["200000", "180", "3.9"];
        MortgageParser parser = new(args);
        Assert.Equal<decimal>(200000m, parser.Principal);
        Assert.Equal<int>(180, parser.Term);
        Assert.Equal(3.9m, parser.Rate);
    }
}