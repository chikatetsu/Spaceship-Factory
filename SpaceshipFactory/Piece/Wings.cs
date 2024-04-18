namespace SpaceshipFactory.Piece;

public class Wings
{
    private readonly string _name;

    public Wings(string name)
    {
        _name = name;
    }

    public override string ToString()
    {
        return _name;
    }
}
