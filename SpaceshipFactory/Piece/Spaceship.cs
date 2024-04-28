namespace SpaceshipFactory.Piece;

public class Spaceship
{
    public readonly string Name;
    public readonly Dictionary<Piece, uint> Pieces;

    public Spaceship(string name, Dictionary<Piece, uint> pieces)
    {
        Name = name;
        Pieces = pieces;
    }

    public Spaceship(string name)
    {
        Name = name;
        Pieces = new Dictionary<Piece, uint>();
    }
    
    public void AddPiece(Piece piece, uint quantity)
    {
        if (!Pieces.TryAdd(piece, quantity))
        {
            Pieces[piece] += quantity;
        }
    }
    
    public bool RemovePiece(Piece piece, uint quantity)
    {
        if (!Pieces.ContainsKey(piece))
        {
            return false;
        }

        Pieces[piece] -= quantity;
        if (Pieces[piece] == 0)
        {
            Pieces.Remove(piece);
        }

        return true;
    }
    
    public override string ToString()
    {
        return Name;
    }
}
