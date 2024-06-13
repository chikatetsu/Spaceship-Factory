using SpaceshipFactory.Command;

namespace TestSpaceshipFactory.Command;

public class ProductionManagerTest
{
    [Fact]
    public void Verify_ReturnsFalse_WhenNoArgsAreProvided()
    {
        var command = new ProductionManager();
        Assert.False(command.Verify(Array.Empty<string>()));
    }

    [Fact]
    public void Verify_ReturnsFalse_WhenOneArgIsProvided()
    {
        var command = new ProductionManager();
        string[] arg = { "Explorer" };
        Assert.False(command.Verify(arg));
    }

    [Fact]
    public void Verify_ReturnsFalse_WhenFirstArgIsNotAQuantity()
    {
        var command = new ProductionManager();
        string[] args = { "any", "Explorer" };
        Assert.False(command.Verify(args));
    }

    [Fact]
    public void Verify_ReturnsTrue_WhenQuantityAndSpaceshipAreProvided()
    {
        var command = new ProductionManager();
        string[] args = { "1", "Explorer" };
        Assert.True(command.Verify(args));
    }

    [Fact]
    public void Verify_ReturnsTrue_WhenMultipleQuantitiesAndSpaceshipsAreProvided()
    {
        var command = new ProductionManager();
        string[] args = { "2", "Explorer", "4", "Speeder" };
        Assert.True(command.Verify(args));
    }
}