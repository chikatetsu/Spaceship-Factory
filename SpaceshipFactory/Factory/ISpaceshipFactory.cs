using SpaceshipFactory.Piece;

namespace SpaceshipFactory
{
    public interface ISpaceshipFactory
    {
        Spaceship CreateSpaceship();
    }
}