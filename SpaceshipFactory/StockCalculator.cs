using SpaceshipFactory.Piece;

namespace SpaceshipFactory;

public static class StockCalculator
{
    public static Dictionary<string, uint> CalculateNeededStocks(string[] spaceshipNames)
    {
        var totalNeededParts = new Dictionary<string, uint>();

        foreach (var name in spaceshipNames)
        {
            Spaceship? spaceship = InstructionManager.ShipModels.Find(spaceship => spaceship.Name == name);
            if (spaceship == null)
            {
                Logger.PrintError($"Unknown spaceship model: {name}");
                continue;
            }
            foreach ((Piece.Piece? piece, uint pieceCount) in spaceship.Pieces)
            {
                if (!totalNeededParts.TryAdd(piece.Name, pieceCount))
                {
                    totalNeededParts[piece.Name] += pieceCount;
                }
            }
        }

        return totalNeededParts;
    }


    public static void PrintNeededStocks(Dictionary<Spaceship, uint>? spaceshipQuantities)
    {
        if (spaceshipQuantities == null)
        {
            return;
        }

        foreach ((Spaceship spaceship, uint quantity) in spaceshipQuantities)
        {
            Logger.PrintResult($"{spaceship.Name} {quantity} :");
            foreach (var piece in spaceship.Pieces)
            {
                for (int i = 0; i < piece.Value * quantity; i++)
                {
                    Logger.PrintResult($"1 {piece.Key.Name}");
                }
            }
        }

        Logger.PrintResult("Total:");
        PrintTotalStocks(spaceshipQuantities);
    }


    private static void PrintTotalStocks(Dictionary<Spaceship, uint> spaceshipQuantities)
    {
        var totalPartsNeeded = new Dictionary<string, uint>();

        foreach ((Spaceship? spaceship, uint spaceshipQuantity) in spaceshipQuantities)
        {
            foreach ((Piece.Piece? piece, uint pieceQuantity) in spaceship.Pieces)
            {
                if (totalPartsNeeded.ContainsKey(piece.Name))
                {
                    totalPartsNeeded[piece.Name] += pieceQuantity * spaceshipQuantity;
                }
                else
                {
                    totalPartsNeeded[piece.Name] = pieceQuantity * spaceshipQuantity;
                }
            }
        }

        foreach (var part in totalPartsNeeded)
        {
            Logger.PrintResult($"{part.Value} {part.Key}");
        }
    }
}
