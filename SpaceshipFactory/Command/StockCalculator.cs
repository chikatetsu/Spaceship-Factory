using SpaceshipFactory.Piece;

namespace SpaceshipFactory;

public class StockCalculator: ICommand
{
    private Dictionary<Spaceship, uint>? _quantityOfSpaceship;


    public void Execute()
    {
        if (_quantityOfSpaceship == null)
        {
            return;
        }
        PrintNeededStocks(_quantityOfSpaceship);
    }

    public bool Verify(IReadOnlyList<string> args)
    {
        if (args.Count == 0)
        {
            Logger.PrintError("NEEDED_STOCKS command expects at least 2 argument");
            return false;
        }
        if (args.Count % 2 != 0)
        {
            Logger.PrintError("NEEDED_STOCKS command expects an even number of arguments");
            return false;
        }
        for (int i = 0; i < args.Count; i += 2)
        {
            if (!int.TryParse(args[i], out int quantity) || quantity < 1)
            {
                Logger.PrintError($"`{args[i]}` is not a valid quantity");
                return false;
            }
        }

        _quantityOfSpaceship = ICommand.MapArgsToQuantityOfSpaceship(args);
        return _quantityOfSpaceship != null;
    }


    public static Dictionary<string, uint> CalculateNeededStocks(string[] spaceshipNames)
    {
        var totalNeededParts = new Dictionary<string, uint>();

        foreach (var name in spaceshipNames)
        {
            var spaceship = InstructionManager.ShipFactories
                .Select(factory => factory.CreateSpaceship())
                .FirstOrDefault(spaceship => spaceship.Name == name);
            
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
