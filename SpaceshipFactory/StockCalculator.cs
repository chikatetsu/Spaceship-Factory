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

    public static void PrintNeededStocks(string[] args)
    {
        var spaceshipQuantities = ParseInput(args);
        foreach ((string? spaceshipName, uint quantity) in spaceshipQuantities)
        {
            Logger.PrintResult($"{spaceshipName} {quantity} :");

            Spaceship? spaceship = InstructionManager.ShipModels.Find(spaceship => spaceship.Name == spaceshipName);
            if (spaceship == null)
            {
                continue;
            }
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

    private static Dictionary<string, uint> ParseInput(IReadOnlyList<string> args)
    {
        var spaceshipQuantities = new Dictionary<string, uint>();
        for (int i = 0; i < args.Count; i += 2)
        {
            if (!uint.TryParse(args[i], out uint quantity) || quantity < 1)
            {
                Logger.PrintError($"Invalid quantity: {args[i]}");
                continue;
            }

            string modelName = args[i + 1];
            if (!InstructionManager.ShipModels.Contains(new Spaceship(modelName)))
            {
                Logger.PrintError($"Spaceship model '{modelName}' is not available.");
                continue;
            }

            if (!spaceshipQuantities.TryAdd(modelName, quantity))
            {
                spaceshipQuantities[modelName] += quantity;
            }
        }
        return spaceshipQuantities;
    }


    private static void PrintTotalStocks(Dictionary<string, uint> spaceshipQuantities)
    {
        var totalPartsNeeded = new Dictionary<string, uint>();

        foreach (var kvp in spaceshipQuantities)
        {
            Spaceship? spaceship = InstructionManager.ShipModels.Find(spaceship => spaceship.Name == kvp.Key);
            if (spaceship == null)
            {
                continue;
            }
            foreach ((Piece.Piece? piece, uint quantity) in spaceship.Pieces)
            {
                if (totalPartsNeeded.ContainsKey(piece.Name))
                {
                    totalPartsNeeded[piece.Name] += quantity * kvp.Value;
                }
                else
                {
                    totalPartsNeeded[piece.Name] = quantity * kvp.Value;
                }
            }
        }

        foreach (var part in totalPartsNeeded)
        {
            Logger.PrintResult($"{part.Value} {part.Key}");
        }
    }
}
