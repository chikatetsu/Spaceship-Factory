namespace SpaceshipFactory.Piece;

public class Thruster
{
    private readonly string _name;

    public Thruster(string name)
    {
        _name = name;
    }

    public override string ToString()
    {
        return _name;
    }
}
