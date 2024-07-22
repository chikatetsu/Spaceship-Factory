using SpaceshipFactory.Piece;

namespace SpaceshipFactory.Factory;

public class SpeederFactory : ISpaceshipFactory
{
    public Spaceship CreateSpaceship()
    {
        var spaceship = new Spaceship("Speeder");
        spaceship.AddPiece(new Hull("Hull_HS1"));
        spaceship.AddPiece(new Engine("Engine_ES1"));
        spaceship.AddPiece(new Wings("Wings_WS1"));
        spaceship.AddPiece(new Wings("Wings_WS1"));
        spaceship.AddPiece(new Thruster("Thruster_TS1"));
        spaceship.AddPiece(new Thruster("Thruster_TS1")); 
        return spaceship;
    }
}