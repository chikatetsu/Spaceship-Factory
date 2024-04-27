namespace SpaceshipFactory.Piece;

public class Piece
{
    private readonly string _name;

    public Piece(string name)
    {
        _name = name;
    }

    public override string ToString()
    {
        return _name;
    }
}