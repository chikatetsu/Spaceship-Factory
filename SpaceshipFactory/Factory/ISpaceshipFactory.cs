using SpaceshipFactory.Piece;

namespace SpaceshipFactory.Factory;

public interface ISpaceshipFactory
{
    Spaceship CreateSpaceship();
}