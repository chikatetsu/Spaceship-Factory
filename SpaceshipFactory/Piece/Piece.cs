namespace SpaceshipFactory.Piece;

public class Piece
{
    public readonly string _name;

    public Piece(string name)
    {
        _name = name;
    }

    public override string ToString()
    {
        return _name;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Piece piece)
        {
            return _name == piece._name;
        }
        
        return false;
    }
    
    public override int GetHashCode()
    {
        return _name.GetHashCode();
    }
}