using SpaceshipFactory.Command;
using SpaceshipFactory.Piece;

namespace TestSpaceshipFactory.Command;

public class StockCalculatorTest
{
    [Fact]
    public void Verify_ReturnsFalse_WhenNoArgsAreProvided()
    {
        var command = new StockCalculator();
        Assert.False(command.Verify(Array.Empty<string>()));
    }

    [Fact]
    public void Verify_ReturnsFalse_WhenOneArgIsProvided()
    {
        var command = new StockCalculator();
        string[] arg = { "Explorer" };
        Assert.False(command.Verify(arg));
    }

    [Fact]
    public void Verify_ReturnsFalse_WhenFirstArgIsNotAQuantity()
    {
        var command = new StockCalculator();
        string[] args = { "any", "Explorer" };
        Assert.False(command.Verify(args));
    }

    [Fact]
    public void Verify_ReturnsTrue_WhenQuantityAndSpaceshipAreProvided()
    {
        var command = new StockCalculator();
        string[] args = { "1", "Explorer" };
        Assert.True(command.Verify(args));
    }

    [Fact]
    public void Verify_ReturnsTrue_WhenMultipleQuantitiesAndSpaceshipsAreProvided()
    {
        var command = new StockCalculator();
        string[] args = { "2", "Explorer", "4", "Speeder" };
        Assert.True(command.Verify(args));
    }



    [Fact]
    public void CalculateNeededStocks_ReturnsCorrectStocks_ForSingleSpaceship()
    {
        var spaceshipNames = new[] { "Explorer" };
        var result = StockCalculator.CalculateNeededStocks(spaceshipNames);

        Assert.Equal(1, (int)result["Hull_HE1"]);
        Assert.Equal(1, (int)result["Engine_EE1"]);
        Assert.Equal(1, (int)result["Wings_WE1"]);
        Assert.Equal(1, (int)result["Thruster_TE1"]);
    }

    [Fact]
    public void CalculateNeededStocks_ReturnsAggregateStocks_ForMultipleSpaceships()
    {
        var spaceshipNames = new[] { "Explorer", "Speeder" };
        var result = StockCalculator.CalculateNeededStocks(spaceshipNames);

        Assert.Equal(1, (int)result["Hull_HE1"]);
        Assert.Equal(1, (int)result["Engine_EE1"]);
        Assert.Equal(1, (int)result["Wings_WE1"]);
        Assert.Equal(1, (int)result["Thruster_TE1"]);
        Assert.Equal(1, (int)result["Hull_HS1"]);
        Assert.Equal(1, (int)result["Engine_ES1"]);
        Assert.Equal(1, (int)result["Wings_WS1"]);
        Assert.Equal(2, (int)result["Thruster_TS1"]);
    }

    [Fact]
    public void PrintNeededStocks_WritesExpectedOutput_ForSingleSpaceship()
    {
        Spaceship? spaceship = ProductionManager.CreateSpaceship("Explorer");
        if (spaceship == null)
        {
            throw new Exception("Spaceship not found");
        }
        Dictionary<Spaceship, uint> spaceshipNames = new()
        {
            {spaceship, 1}
        };
        var output = new StringWriter();
        Console.SetOut(output);

        StockCalculator.PrintNeededStocks(spaceshipNames);

        var outputString = output.ToString();
        Assert.Contains("Explorer 1 :", outputString);
        Assert.Contains("Total:", outputString);
        Assert.Contains("1 Hull_HE1", outputString);
        Assert.Contains("1 Engine_EE1", outputString);
        Assert.Contains("1 Wings_WE1", outputString);
        Assert.Contains("1 Thruster_TE1", outputString);
    }
}
