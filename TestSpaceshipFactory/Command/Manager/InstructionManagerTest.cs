using SpaceshipFactory.Command;
using SpaceshipFactory.Command.Manager;

namespace TestSpaceshipFactory.Command;

public class InstructionManagerTest
{
    [Fact]
    public void Verify_ReturnsFalse_WhenNoArgsAreProvided()
    {
        var command = new InstructionManager();
        Assert.False(command.Verify(Array.Empty<string>()));
    }

    [Fact]
    public void Verify_ReturnsFalse_WhenOneArgIsProvided()
    {
        var command = new InstructionManager();
        string[] arg = { "Explorer" };
        Assert.False(command.Verify(arg));
    }

    [Fact]
    public void Verify_ReturnsFalse_WhenFirstArgIsNotAQuantity()
    {
        var command = new InstructionManager();
        string[] args = { "any", "Explorer" };
        Assert.False(command.Verify(args));
    }

    [Fact]
    public void Verify_ReturnsTrue_WhenQuantityAndSpaceshipAreProvided()
    {
        var command = new InstructionManager();
        string[] args = { "1", "Explorer" };
        Assert.True(command.Verify(args));
    }

    [Fact]
    public void Verify_ReturnsTrue_WhenMultipleQuantitiesAndSpaceshipsAreProvided()
    {
        var command = new InstructionManager();
        string[] args = { "2", "Explorer", "4", "Speeder" };
        Assert.True(command.Verify(args));
    }
}