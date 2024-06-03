using SpaceshipFactory;
using SpaceshipFactory.Piece;

public class SpeederFactory : ISpaceshipFactory
{
    public Spaceship CreateSpaceship()
    {
        return new Spaceship("Speeder", new Dictionary<Piece, uint>
        {
            { new Hull("Hull_HS1"), 1 },
            { new Engine("Engine_ES1"), 1 },
            { new Wings("Wings_WS1"), 1 },
            { new Thruster("Thruster_TS1"), 2 }
        });
    }
}
