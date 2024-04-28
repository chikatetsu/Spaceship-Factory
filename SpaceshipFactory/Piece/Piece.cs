namespace SpaceshipFactory.Piece;

public class Piece
{
    public readonly string Name;

    protected Piece(string name)
    {
        Name = name;
    }

    public override string ToString()
    {
        return Name;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Piece piece)
        {
            return Name == piece.Name;
        }
        
        return false;
    }
    
    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}