using SpaceshipFactory.Piece;

namespace SpaceshipFactory;

public static class Stock
{
    private static readonly Dictionary<Spaceship, uint> Spaceships = new();
    private static readonly Dictionary<Piece.Piece, uint> Pieces = new()
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

    public static bool Add(Spaceship spaceship, uint quantity)
    {
        if (quantity == 0)
        {
            return false;
        }
        if (!Spaceships.TryAdd(spaceship, quantity))
        {
            Spaceships[spaceship] += quantity;
        }

        return true;
    }
    
    public static bool Add(Piece.Piece piece, uint quantity)
    {
        if (quantity == 0)
        {
            return false;
        }
        if (!Pieces.TryAdd(piece, quantity))
        {
            Pieces[piece] += quantity;
        }

        return true;
    }
    
    public static bool Remove(Piece.Piece piece, uint quantity)
    {
        if (!Pieces.ContainsKey(piece))
        {
            Logger.PrintError($"Piece {piece} is not in stock.");
            return false;
        }
        if (quantity == 0)
        {
            return false;
        }
        if (Pieces[piece] < quantity)
        {
            Logger.PrintError($"Not enough {piece} in stock.");
            return false;
        }
        
        Logger.PrintInstruction("GET_OUT_STOCK", $"{quantity} {piece}");
        Pieces[piece] -= quantity;
        return true;
    }

    public static string GetStocks()
    {
        string str = "";
        foreach (var kv in Spaceships)
        {
            if (kv.Value == 0)
            {
                continue;
            }
            str += $"{kv.Value} {kv.Key}\n";
        }
        foreach (var kv in Pieces)
        {
            if (kv.Value == 0)
            {
                continue;
            }
            str += $"{kv.Value} {kv.Key}\n";
        }
        return str;
    }

    public static bool Verify(Spaceship spaceshipModel, int quantity)
    {
        return true;
    }
}
