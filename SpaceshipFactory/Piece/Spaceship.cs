namespace SpaceshipFactory.Piece;

public class Spaceship
{
    private readonly string _name;
    private Hull _hull;
    private Engine _engine;
    private Wings _wings;
    private Thruster _thruster;

    public Spaceship(string name, Hull hull, Engine engine, Wings wings, Thruster thruster)
    {
        _name = name;
        _hull = hull;
        _engine = engine;
        _wings = wings;
        _thruster = thruster;
    }

    public override string ToString()
    {
        return _name;
    }
}
