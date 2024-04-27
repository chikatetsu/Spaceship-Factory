namespace SpaceshipFactory.Piece;

public class Spaceship
{
    public readonly string _name;
    public Dictionary<Piece?, uint> _pieces;

    public Spaceship(string name, Dictionary<Piece?, uint> pieces)
    {
        _name = name;
        _pieces = pieces;
    }

    public Spaceship(string name)
    {
        _pieces = new Dictionary<Piece?, uint>();
        _name = name;
    }
    
    public void AddPiece(Piece? piece, uint quantity)
    {
        if (_pieces.ContainsKey(piece))
        {
            _pieces[piece] += quantity;
            return;
        }
        _pieces.Add(piece, quantity);
    }
    
    public void RemovePiece(Piece? piece, uint quantity)
    {
        if (_pieces.ContainsKey(piece))
        {
            _pieces[piece] -= quantity;
            if (_pieces[piece] == 0)
            {
                _pieces.Remove(piece);
            }
        }
    }
    
    public override string ToString()
    {
        return _name;
    }
}
