using SpaceshipFactory.Piece;

namespace TestSpaceshipFactory;

public class StockCalculatorTest
{
    [Fact]
    public void CalculateNeededStocks_ReturnsCorrectStocksForSingleSpaceship()
    {
        var spaceshipNames = new[] { "Explorer" };
        var result = StockCalculator.CalculateNeededStocks(spaceshipNames);

        Assert.Equal(1, (int)result["Hull_HE1"]);
        Assert.Equal(1, (int)result["Engine_EE1"]);
        Assert.Equal(1, (int)result["Wings_WE1"]);
        Assert.Equal(1, (int)result["Thruster_TE1"]);
    }

    [Fact]
    public void CalculateNeededStocks_ReturnsAggregateStocksForMultipleSpaceships()
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
    public void PrintNeededStocks_WritesExpectedOutputForSingleSpaceship()
    {
        Spaceship? spaceship = InstructionManager.ShipModels.Find(spaceship => spaceship.Name == "Explorer");
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
