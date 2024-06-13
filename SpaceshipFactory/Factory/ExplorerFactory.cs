using SpaceshipFactory.Piece;

namespace SpaceshipFactory.Factory;

public class ExplorerFactory : ISpaceshipFactory
{
    public Spaceship CreateSpaceship()
    {
        var spaceship = new Spaceship("Explorer");
        spaceship.AddPiece(new Hull("Hull_HE1"));
        spaceship.AddPiece(new Engine("Engine_EE1"));
        spaceship.AddPiece(new Wings("Wings_WE1"));
        spaceship.AddPiece(new Thruster("Thruster_TE1"));
        return spaceship;
    }
}