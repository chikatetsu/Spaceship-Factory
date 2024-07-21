using SpaceshipFactory.Command;
using SpaceshipFactory.Command.Manager;
using SpaceshipFactory.Piece;

namespace TestSpaceshipFactory.Command;

public class ICommandExample : ICommand
{
    public void Execute()
    {
        throw new NotImplementedException();
    }

    public bool Verify(IReadOnlyList<string> args)
    {
        throw new NotImplementedException();
    }



    [Fact]
    public void MapArgsToQuantityOfSpaceship_ReturnsNull_WhenNoArgsAreProvided()
    {
        string[] args = Array.Empty<string>();
        Assert.Null(ICommand.MapArgsToQuantityOfSpaceship(args));
    }

    [Fact]
    public void MapArgsToQuantityOfSpaceship_ReturnsNull_WhenOneArgIsProvided()
    {
        string[] arg = { "Explorer" };
        Assert.Null(ICommand.MapArgsToQuantityOfSpaceship(arg));
    }

    [Fact]
    public void MapArgsToQuantityOfSpaceship_ReturnsNull_WhenFirstArgIsNotAQuantity()
    {
        string[] args = { "any", "Explorer" };
        Assert.Null(ICommand.MapArgsToQuantityOfSpaceship(args));
    }

    [Fact]
    public void MapArgsToQuantityOfSpaceship_ReturnsNull_WhenSecondArgIsNotAKnownSpaceship()
    {
        string[] args = { "1", "Unknown" };
        Assert.Null(ICommand.MapArgsToQuantityOfSpaceship(args));
    }

    [Fact]
    public void MapArgsToQuantityOfSpaceship_IgnoreUnknownSpaceship_WhenProvided()
    {
        var expected = new Dictionary<Spaceship, uint>
        {
            { ProductionManager.CreateSpaceship("Explorer"), 5 }
        };

        string[] args = { "5", "Explorer" , "1", "Unknown" };
        var reality = ICommand.MapArgsToQuantityOfSpaceship(args);

        Assert.NotNull(reality);
        foreach ((Spaceship spaceship, uint quantity) in reality)
        {
            Assert.True(expected.ContainsKey(spaceship));
            Assert.True(expected[spaceship] == quantity);
        }
    }

    [Fact]
    public void MapArgsToQuantityOfSpaceship_ReturnsDictionary_WhenQuantityAndSpaceshipAreProvided()
    {
        const string spaceshipName = "Explorer";
        var expected = new Dictionary<Spaceship, uint>
        {
            { ProductionManager.CreateSpaceship(spaceshipName), 1 }
        };

        string[] args = { "1", spaceshipName };
        var reality = ICommand.MapArgsToQuantityOfSpaceship(args);

        Assert.NotNull(reality);
        foreach ((Spaceship spaceship, uint quantity) in reality)
        {
            Assert.True(expected.ContainsKey(spaceship));
            Assert.True(expected[spaceship] == quantity);
        }
    }

    [Fact]
    public void MapArgsToQuantityOfSpaceship_ReturnsDictionary_WhenMultipleQuantitiesAndSpaceshipsAreProvided()
    {
        var expected = new Dictionary<Spaceship, uint>
        {
            { ProductionManager.CreateSpaceship("Explorer"), 2 },
            { ProductionManager.CreateSpaceship("Speeder"), 4 },
        };

        string[] args = { "2", "Explorer", "4", "Speeder" };
        var reality = ICommand.MapArgsToQuantityOfSpaceship(args);

        Assert.NotNull(reality);
        foreach ((Spaceship spaceship, uint quantity) in reality)
        {
            Assert.True(expected.ContainsKey(spaceship));
            Assert.True(expected[spaceship] == quantity);
        }
    }

    [Fact]
    public void MapArgsToQuantityOfSpaceship_AddsSpaceshipQuantity_WhenSameSpaceshipIsProvidedMultipleTimes()
    {
        var expected = new Dictionary<Spaceship, uint>
        {
            { ProductionManager.CreateSpaceship("Explorer"), 6 }
        };

        string[] args = { "2", "Explorer", "4", "Explorer" };
        var reality = ICommand.MapArgsToQuantityOfSpaceship(args);

        Assert.NotNull(reality);
        foreach ((Spaceship spaceship, uint quantity) in reality)
        {
            Assert.True(expected.ContainsKey(spaceship));
            Assert.True(expected[spaceship] == quantity);
        }
    }
}
