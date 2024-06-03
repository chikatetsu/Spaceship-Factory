using SpaceshipFactory;
using SpaceshipFactory.Piece;

public class ExplorerFactory : ISpaceshipFactory
{
    public Spaceship CreateSpaceship()
    {
        return new Spaceship("Explorer", new Dictionary<Piece, uint>
        {
            { new Hull("Hull_HE1"), 1 },
            { new Engine("Engine_EE1"), 1 },
            { new Wings("Wings_WE1"), 1 },
            { new Thruster("Thruster_TE1"), 1 }
        });
    }
}
