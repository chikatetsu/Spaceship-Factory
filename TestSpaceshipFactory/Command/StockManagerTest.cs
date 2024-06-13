using SpaceshipFactory.Command;

namespace TestSpaceshipFactory.Command;

public class StockCommandTest
{
    [Fact]
    public void Verify_ReturnsTrue_WhenNoArgsAreProvided()
    {
        var command = new StockManager();
        Assert.True(command.Verify(Array.Empty<string>()));
    }

    [Fact]
    public void Verify_ReturnsFalse_WhenArgsAreProvided()
    {
        var command = new StockManager();
        string[] oneArg = { "any" };
        Assert.False(command.Verify(oneArg));
        string[] multipleArgs = { "any1", "any2", "any3" };
        Assert.False(command.Verify(multipleArgs));
    }
}