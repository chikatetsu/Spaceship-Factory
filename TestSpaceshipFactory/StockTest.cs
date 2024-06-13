using SpaceshipFactory.Command;
using SpaceshipFactory.Piece;

namespace TestSpaceshipFactory;

public class StockTest
{
    [Fact]
    public void AddSpaceship_AddsNewSpaceship_WhenNotExists()
    {
        Assert.True(Stock.Instance.Add(new Spaceship("Spaceship"), 2));
        //TODO: Check stock update
    }

    [Fact]
    public void AddSpaceship_AddsQuantity_WhenExists()
    {
        Assert.True(Stock.Instance.Add(new Spaceship("Spaceship"), 1));
        Assert.True(Stock.Instance.Add(new Spaceship("Spaceship"), 1));
        //TODO: Check stock update
    }

    [Fact]
    public void AddSpaceship_DoesNotAdd_WhenQuantityIsZero()
    {
        Assert.False(Stock.Instance.Add(new Spaceship("Spaceship"), 0));
        //TODO: Check stock update
    }

    [Fact]
    public void AddPiece_AddsNewPiece_WhenNotExists()
    {
        Assert.True(Stock.Instance.Add(new Engine("Engine"), 2));
        //TODO: Check stock update
    }

    [Fact]
    public void AddPiece_AddsQuantity_WhenExists()
    {
        Assert.True(Stock.Instance.Add(new Hull("Hull"), 1));
        Assert.True(Stock.Instance.Add(new Hull("Hull"), 1));
        //TODO: Check stock update
    }

    [Fact]
    public void AddPiece_DoesNotAdd_WhenQuantityIsZero()
    {
        Assert.False(Stock.Instance.Add(new Thruster("Thruster"), 0));
        //TODO: Check stock update
    }



    [Fact]
    public void RemovePiece_RemovesQuantity_WhenExists()
    {
        Assert.True(Stock.Instance.Add(new Wings("Wings"), 2));
        Assert.True(Stock.Instance.Remove(new Wings("Wings"), 1));
        //TODO: Check stock update
    }

    [Fact]
    public void RemovePiece_RemovesObject_WhenQuantityIsTheSame()
    {
        Assert.True(Stock.Instance.Add(new Engine("Engine"), 2));
        Assert.True(Stock.Instance.Remove(new Engine("Engine"), 2));
        //TODO: Check stock update
    }

    [Fact]
    public void RemovePiece_ReturnsFalse_WhenQuantityIsZero()
    {
        Assert.True(Stock.Instance.Add(new Hull("Hull"), 2));
        Assert.False(Stock.Instance.Remove(new Hull("Hull"), 0));
        //TODO: Check stock update
    }

    [Fact]
    public void RemovePiece_ReturnsFalse_WhenPieceIsNotInStock()
    {
        Assert.False(Stock.Instance.Remove(new Thruster("Thruster"), 0));
        //TODO: Check stock update
    }

    [Fact]
    public void RemovePiece_ReturnsFalse_WhenNotEnoughQuantityIsInStock()
    {
        Assert.True(Stock.Instance.Add(new Wings("Wings"), 1));
        Assert.False(Stock.Instance.Remove(new Wings("Wings"), 2));
        //TODO: Check stock update
    }
}