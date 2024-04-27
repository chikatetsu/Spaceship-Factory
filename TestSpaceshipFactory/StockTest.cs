using SpaceshipFactory.Piece;

namespace TestSpaceshipFactory;

public class StockTest
{
    [Fact]
    public void AddSpaceshipAddsNewSpaceshipWhenNotExists()
    {
    }

    [Fact]
    public void AddSpaceshipAddsQuantityWhenExists()
    {
    }

    [Fact]
    public void AddSpaceshipDoesNotAddWhenQuantityIsZero()
    {
        Assert.False(Stock.Add(new Spaceship("Spaceship1"), 0));
    }

    [Fact]
    public void AddPieceAddsNewPieceWhenNotExists()
    {
    }

    [Fact]
    public void AddPieceAddsQuantityWhenExists()
    {
    }

    [Fact]
    public void AddPieceDoesNotAddWhenQuantityIsZero()
    {
        Assert.False(Stock.Add(new Engine("Engine1"), 0));
    }
}