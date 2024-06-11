using SpaceshipFactory.Piece;

namespace SpaceshipFactory.Factory;

public class ExplorerFactory : ISpaceshipFactory
{
    public Spaceship CreateSpaceship()
    {
        return new Spaceship("Explorer", new Dictionary<Piece.Piece, uint>
        {
            { new Hull("Hull_HE1"), 1 },
            { new Engine("Engine_EE1"), 1 },
            { new Wings("Wings_WE1"), 1 },
            { new Thruster("Thruster_TE1"), 1 }
        });
    }
}