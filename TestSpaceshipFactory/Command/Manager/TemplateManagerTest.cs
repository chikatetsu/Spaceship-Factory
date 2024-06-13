using SpaceshipFactory.Command;

namespace TestSpaceshipFactory.Command;

public class TemplateManagerTest
{
    [Fact]
    public void Verify_ReturnsFalse_WhenNoArgsAreProvided()
    {
        var command = new TemplateManager();
        Assert.False(command.Verify(Array.Empty<string>()));
    }

    [Fact]
    public void Verify_ReturnsFalse_WhenOneArgIsProvided()
    {
        var command = new TemplateManager();
        string[] arg = { "Hull_HE1" };
        Assert.False(command.Verify(arg));
    }

    [Fact]
    public void Verify_ReturnsFalse_WhenSecondArgIsNotAKnownPiece()
    {
        var command = new TemplateManager();
        string[] args = { "template", "NotAPiece" };
        Assert.False(command.Verify(args));
    }

    [Fact]
    public void Verify_ReturnsTrue_WhenKnownPieceIsProvided()
    {
        var command = new TemplateManager();
        string[] args = { "template", "Hull_HE1" };
        Assert.True(command.Verify(args));
    }

    [Fact]
    public void Verify_ReturnsTrue_WhenMultipleKnownPiecesAreProvided()
    {
        var command = new TemplateManager();
        string[] args = { "template", "Hull_HE1", "Engine_EE1" };
        Assert.True(command.Verify(args));
    }
}