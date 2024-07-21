using SpaceshipFactory.Piece;
using System.Collections.Generic;
using System.Linq;
using SpaceshipFactory.Command.Manager;

namespace SpaceshipFactory.Command;

public class StockCalculator: ICommand
{
    private Dictionary<Spaceship, uint>? _quantityOfSpaceship;

    public void Execute()
    {
        if (_quantityOfSpaceship != null)
        {
            PrintNeededStocks(_quantityOfSpaceship);
        }
    }

    public bool Verify(IReadOnlyList<string> args)
    {
        if (args.Count == 0)
        {
            Logger.PrintError("NEEDED_STOCKS command expects at least one quantity and one spaceship");
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

            if (spaceship.Hull != null)
            {
                AddPieceToTotalNeededParts(totalNeededParts, spaceship.Hull.Name, 1);
            }

            foreach (var engine in spaceship.Engines)
            {
                AddPieceToTotalNeededParts(totalNeededParts, engine.Name, 1);
            }

            foreach (var wings in spaceship.Wings)
            {
                AddPieceToTotalNeededParts(totalNeededParts, wings.Name, 1);
            }

            foreach (var thruster in spaceship.Thrusters)
            {
                AddPieceToTotalNeededParts(totalNeededParts, thruster.Name, 1);
            }
        }

        return totalNeededParts;
    }

    private static void AddPieceToTotalNeededParts(Dictionary<string, uint> totalNeededParts, string pieceName, uint pieceCount)
    {
        if (!totalNeededParts.TryAdd(pieceName, pieceCount))
        {
            totalNeededParts[pieceName] += pieceCount;
        }
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

            if (spaceship.Hull != null)
            {
                PrintPieceStock(spaceship.Hull.Name, 1 * quantity);
            }

            foreach (var engine in spaceship.Engines)
            {
                PrintPieceStock(engine.Name, 1 * quantity);
            }

            foreach (var wings in spaceship.Wings)
            {
                PrintPieceStock(wings.Name, 1 * quantity);
            }

            foreach (var thruster in spaceship.Thrusters)
            {
                PrintPieceStock(thruster.Name, 1 * quantity);
            }
        }

        Logger.PrintResult("Total:");
        PrintTotalStocks(spaceshipQuantities);
    }

    private static void PrintPieceStock(string pieceName, uint quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            Logger.PrintResult($"1 {pieceName}");
        }
    }

    private static void PrintTotalStocks(Dictionary<Spaceship, uint> spaceshipQuantities)
    {
        var totalPartsNeeded = new Dictionary<string, uint>();

        foreach ((Spaceship? spaceship, uint spaceshipQuantity) in spaceshipQuantities)
        {
            if (spaceship.Hull != null)
            {
                AddPieceToTotalNeededParts(totalPartsNeeded, spaceship.Hull.Name, 1 * spaceshipQuantity);
            }

            foreach (var engine in spaceship.Engines)
            {
                AddPieceToTotalNeededParts(totalPartsNeeded, engine.Name, 1 * spaceshipQuantity);
            }

            foreach (var wings in spaceship.Wings)
            {
                AddPieceToTotalNeededParts(totalPartsNeeded, wings.Name, 1 * spaceshipQuantity);
            }

            foreach (var thruster in spaceship.Thrusters)
            {
                AddPieceToTotalNeededParts(totalPartsNeeded, thruster.Name, 1 * spaceshipQuantity);
            }
        }

        foreach (var part in totalPartsNeeded)
        {
            Logger.PrintResult($"{part.Value} {part.Key}");
        }
    }
}

