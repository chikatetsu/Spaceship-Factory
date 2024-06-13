using SpaceshipFactory.Command;

namespace TestSpaceshipFactory.Command;

public class VerificationManagerTest
{
    [Fact]
    public void Verify_ReturnsFalse_WhenNoArgsAreProvided()
    {
        var command = new VerificationManager();
        Assert.False(command.Verify(Array.Empty<string>()));
    }

    [Fact]
    public void Verify_ReturnsTrue_WhenArgsAreProvided()
    {
        var command = new VerificationManager();
        string[] oneArg = { "any" };
        Assert.True(command.Verify(oneArg));
        string[] multipleArgs = { "any1", "any2", "any3" };
        Assert.True(command.Verify(multipleArgs));
    }
}