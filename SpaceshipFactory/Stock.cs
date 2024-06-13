using SpaceshipFactory.Piece;

namespace SpaceshipFactory;

public class Stock
{
    private static Stock? _instance;
    private static readonly object Lock = new();
    private readonly Dictionary<Spaceship, uint> _spaceships;
    private readonly Dictionary<Piece.Piece, uint> _pieces;

    private Stock()
    {
        _spaceships = new Dictionary<Spaceship, uint>();
        _pieces = new Dictionary<Piece.Piece, uint>
        {
            { new Engine("Engine_EE1"), 5 },
            { new Engine("Engine_ES1"), 9 },
            { new Engine("Engine_EC1"), 11 },
            { new Hull("Hull_HE1"), 10 },
            { new Hull("Hull_HS1"), 15 },
            { new Hull("Hull_HC1"), 0 },
            { new Thruster("Thruster_TE1"), 20 },
            { new Thruster("Thruster_TS1"), 5 },
            { new Thruster("Thruster_TC1"), 18 },
            { new Wings("Wings_WE1"), 32 },
            { new Wings("Wings_WS1"), 16 },
            { new Wings("Wings_WC1"), 24 },
        };
    }
    
    public static Stock Instance
    {
        get
        {
            lock (Lock)
            {
                if (_instance == null)
                {
                    _instance = new Stock();
                }
                return _instance;
            }
        }
    }
    

    public bool Add(Spaceship spaceship, uint quantity)
    {
        if (quantity == 0)
        {
            return false;
        }
        if (!_spaceships.TryAdd(spaceship, quantity))
        {
            _spaceships[spaceship] += quantity;
        }

        return true;
    }
    
    public bool Add(Piece.Piece piece, uint quantity)
    {
        if (quantity == 0)
        {
            return false;
        }
        if (!_pieces.TryAdd(piece, quantity))
        {
            _pieces[piece] += quantity;
        }
        return true;
    }
    
    public bool Remove(Piece.Piece piece, uint quantity)
    {
        if (quantity == 0)
        {
            return false;
        }
        if (!_pieces.ContainsKey(piece))
        {
            Logger.PrintError($"Piece {piece} is not in stock.");
            return false;
        }
        if (_pieces[piece] < quantity)
        {
            Logger.PrintError($"Not enough {piece} in stock.");
            return false;
        }
        
        //Logger.PrintInstruction("GET_OUT_STOCK", $"{quantity} {piece}");
        _pieces[piece] -= quantity;
        return true;
    }

    public string GetStocks()
    {
        string str = "";
        foreach ((Spaceship spaceship, uint quantity) in _spaceships)
        {
            if (quantity == 0)
            {
                continue;
            }
            str += $"{quantity} {spaceship}\n";
        }
        foreach ((Piece.Piece piece, uint quantity) in _pieces)
        {
            if (quantity == 0)
            {
                continue;
            }
            str += $"{quantity} {piece}\n";
        }
        return str;
    }

    public bool IsStockSufficient(Spaceship model, uint spaceshipQuantity)
    {
        foreach ((Piece.Piece? piece, uint pieceQuantity) in model.Pieces)
        {
            if (!_pieces.ContainsKey(piece))
            {
                return false;
            }
            if (_pieces[piece] < pieceQuantity * spaceshipQuantity)
            {
                return false;
            }
        }
        return true;
    }
}
