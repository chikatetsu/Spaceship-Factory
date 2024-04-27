namespace SpaceshipFactory.Piece;

public class Hull
{
    private readonly string _name;

    public Hull(string name)
    {
        _name = name;
    }

    public override string ToString()
    {
        return _name;
    }
}
