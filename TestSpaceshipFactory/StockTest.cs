using SpaceshipFactory.Piece;

namespace TestSpaceshipFactory;

public class StockTest
{
    [Fact]
    public void AddSpaceshipAddsNewSpaceshipWhenNotExists()
    {
        Assert.True(Stock.Add(new Spaceship("Spaceship"), 2));
        //TODO: Check stock update
    }

    [Fact]
    public void AddSpaceshipAddsQuantityWhenExists()
    {
        Assert.True(Stock.Add(new Spaceship("Spaceship"), 1));
        Assert.True(Stock.Add(new Spaceship("Spaceship"), 1));
        //TODO: Check stock update
    }

    [Fact]
    public void AddSpaceshipDoesNotAddWhenQuantityIsZero()
    {
        Assert.False(Stock.Add(new Spaceship("Spaceship"), 0));
        //TODO: Check stock update
    }

    [Fact]
    public void AddPieceAddsNewPieceWhenNotExists()
    {
        Assert.True(Stock.Add(new Engine("Engine"), 2));
        //TODO: Check stock update
    }

    [Fact]
    public void AddPieceAddsQuantityWhenExists()
    {
        Assert.True(Stock.Add(new Hull("Hull"), 1));
        Assert.True(Stock.Add(new Hull("Hull"), 1));
        //TODO: Check stock update
    }

    [Fact]
    public void AddPieceDoesNotAddWhenQuantityIsZero()
    {
        Assert.False(Stock.Add(new Thruster("Thruster"), 0));
        //TODO: Check stock update
    }



    [Fact]
    public void RemovePieceRemovesQuantityWhenExists()
    {
        Assert.True(Stock.Add(new Wings("Wings"), 2));
        Assert.True(Stock.Remove(new Wings("Wings"), 1));
        //TODO: Check stock update
    }

    [Fact]
    public void RemovePieceRemovesObjectWhenQuantityIsTheSame()
    {
        Assert.True(Stock.Add(new Engine("Engine"), 2));
        Assert.True(Stock.Remove(new Engine("Engine"), 2));
        //TODO: Check stock update
    }

    [Fact]
    public void RemovePieceReturnsFalseWhenQuantityIsZero()
    {
        Assert.True(Stock.Add(new Hull("Hull"), 2));
        Assert.False(Stock.Remove(new Hull("Hull"), 0));
        //TODO: Check stock update
    }

    [Fact]
    public void RemovePieceReturnsFalseWhenPieceIsNotInStock()
    {
        Assert.False(Stock.Remove(new Thruster("Thruster"), 0));
        //TODO: Check stock update
    }

    [Fact]
    public void RemovePieceReturnsFalseWhenNotEnoughQuantityIsInStock()
    {
        Assert.True(Stock.Add(new Wings("Wings"), 1));
        Assert.False(Stock.Remove(new Wings("Wings"), 2));
        //TODO: Check stock update
    }
}