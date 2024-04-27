namespace SpaceshipFactory.Piece;

public class Engine
{
    private readonly string _name;

    public Engine(string name)
    {
        _name = name;
    }

    public override string ToString()
    {
        return _name;
    }
}
