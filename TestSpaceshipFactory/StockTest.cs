using SpaceshipFactory.Piece;

namespace TestSpaceshipFactory;

public class StockTest
{
    [Fact]
    public void AddSpaceshipAddsNewSpaceshipWhenNotExists()
    {
        Assert.True(Stock.Instance.Add(new Spaceship("Spaceship"), 2));
        //TODO: Check stock update
    }

    [Fact]
    public void AddSpaceshipAddsQuantityWhenExists()
    {
        Assert.True(Stock.Instance.Add(new Spaceship("Spaceship"), 1));
        Assert.True(Stock.Instance.Add(new Spaceship("Spaceship"), 1));
        //TODO: Check stock update
    }

    [Fact]
    public void AddSpaceshipDoesNotAddWhenQuantityIsZero()
    {
        Assert.False(Stock.Instance.Add(new Spaceship("Spaceship"), 0));
        //TODO: Check stock update
    }

    [Fact]
    public void AddPieceAddsNewPieceWhenNotExists()
    {
        Assert.True(Stock.Instance.Add(new Engine("Engine"), 2));
        //TODO: Check stock update
    }

    [Fact]
    public void AddPieceAddsQuantityWhenExists()
    {
        Assert.True(Stock.Instance.Add(new Hull("Hull"), 1));
        Assert.True(Stock.Instance.Add(new Hull("Hull"), 1));
        //TODO: Check stock update
    }

    [Fact]
    public void AddPieceDoesNotAddWhenQuantityIsZero()
    {
        Assert.False(Stock.Instance.Add(new Thruster("Thruster"), 0));
        //TODO: Check stock update
    }



    [Fact]
    public void RemovePieceRemovesQuantityWhenExists()
    {
        Assert.True(Stock.Instance.Add(new Wings("Wings"), 2));
        Assert.True(Stock.Instance.Remove(new Wings("Wings"), 1));
        //TODO: Check stock update
    }

    [Fact]
    public void RemovePieceRemovesObjectWhenQuantityIsTheSame()
    {
        Assert.True(Stock.Instance.Add(new Engine("Engine"), 2));
        Assert.True(Stock.Instance.Remove(new Engine("Engine"), 2));
        //TODO: Check stock update
    }

    [Fact]
    public void RemovePieceReturnsFalseWhenQuantityIsZero()
    {
        Assert.True(Stock.Instance.Add(new Hull("Hull"), 2));
        Assert.False(Stock.Instance.Remove(new Hull("Hull"), 0));
        //TODO: Check stock update
    }

    [Fact]
    public void RemovePieceReturnsFalseWhenPieceIsNotInStock()
    {
        Assert.False(Stock.Instance.Remove(new Thruster("Thruster"), 0));
        //TODO: Check stock update
    }

    [Fact]
    public void RemovePieceReturnsFalseWhenNotEnoughQuantityIsInStock()
    {
        Assert.True(Stock.Instance.Add(new Wings("Wings"), 1));
        Assert.False(Stock.Instance.Remove(new Wings("Wings"), 2));
        //TODO: Check stock update
    }
}