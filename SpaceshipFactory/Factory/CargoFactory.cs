using SpaceshipFactory;
using SpaceshipFactory.Piece;

public class CargoFactory : ISpaceshipFactory
{
    public Spaceship CreateSpaceship()
    {
        var spaceship = new Spaceship("Cargo");
        spaceship.AddPiece(new Hull("Hull_HC1"));
        spaceship.AddPiece(new Engine("Engine_EC1"));
        spaceship.AddPiece(new Wings("Wings_WC1"));
        spaceship.AddPiece(new Thruster("Thruster_TC1"));
        return spaceship;
    }
}