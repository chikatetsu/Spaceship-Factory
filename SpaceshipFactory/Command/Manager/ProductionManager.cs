using SpaceshipFactory.Factory;
using SpaceshipFactory.Piece;

namespace SpaceshipFactory.Command.Manager;

public class ProductionManager: ICommand
{
    private static readonly Dictionary<string, Spaceship> CustomTemplates = new();
    private Dictionary<Spaceship, uint>? _quantityOfSpaceship;
    private static readonly Dictionary<string, ISpaceshipFactory> Factories = new()
    {
        { "Explorer", new ExplorerFactory() },
        { "Speeder", new SpeederFactory() },
        { "Cargo", new CargoFactory() }
    };


    public void Execute()
    {
        if (_quantityOfSpaceship != null)
        {
            foreach ((Spaceship model, uint quantity) in _quantityOfSpaceship)
            {
                Produce(model, quantity);
            }
        }
    }

    public bool Verify(IReadOnlyList<string> args)
    {
        if (args.Count == 0)
        {
            Logger.PrintError("PRODUCE command expects at least one quantity and one spaceship");
            return false;
        }
        if (args.Count % 2 != 0)
        {
            Logger.PrintError("PRODUCE command expects an even number of arguments");
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


    public static void Produce(Spaceship model, uint quantityToProduce)
    {
        var stock = Stock.Instance;
        if (!stock.IsStockSufficient(model, quantityToProduce))
        {
            Logger.PrintError("Unable to start production due to insufficient stock");
            return;
        }

        for (var i = 0; i < quantityToProduce; i++)
        {
            Spaceship newSpaceship = new(model.Name);

            foreach (var piece in model.Engines)
            {
                if (!newSpaceship.AddPiece(piece))
                {
                    Logger.PrintError($"Failed to add {piece.GetType().Name}");
                    return;
                }
            }
            foreach (var piece in model.Wings)
            {
                if (!newSpaceship.AddPiece(piece))
                {
                    Logger.PrintError($"Failed to add {piece.GetType().Name}");
                    return;
                }
            }
            foreach (var piece in model.Thrusters)
            {
                if (!newSpaceship.AddPiece(piece))
                {
                    Logger.PrintError($"Failed to add {piece.GetType().Name}");
                    return;
                }
            }
            if (model.Hull != null)
            {
                newSpaceship.AddPiece(model.Hull);
            }

            if (!newSpaceship.IsValid())
            {
                Logger.PrintError($"Invalid spaceship configuration for {newSpaceship.Name}");
                return;
            }

            stock.Add(newSpaceship, 1);
        }
        Logger.PrintResult("STOCK_UPDATED");
    }
        
    public static Spaceship? CreateSpaceship(string type)
    {
        if (CustomTemplates.TryGetValue(type, out Spaceship? customTemplate))
        {
            Spaceship newSpaceship = new(customTemplate.Name);
            foreach (var piece in customTemplate.Engines) newSpaceship.AddPiece(piece);
            foreach (var piece in customTemplate.Wings) newSpaceship.AddPiece(piece);
            foreach (var piece in customTemplate.Thrusters) newSpaceship.AddPiece(piece);
            if (customTemplate.Hull != null) newSpaceship.AddPiece(customTemplate.Hull);
            return newSpaceship;
        }

        if (!Factories.TryGetValue(type, out ISpaceshipFactory? factory))
        {
            return null;
        }
        return factory.CreateSpaceship();
    }
    
    public static void AddTemplate(string name, List<Piece.Piece?> pieces)
    {
        Spaceship newTemplate = new(name);
        foreach (var piece in pieces)
        {
            if (!newTemplate.AddPiece(piece))
            {
                Logger.PrintError($"Failed to add {piece.GetType().Name} to template {name}");
                return;
            }
        }
        if (newTemplate.IsValid())
        {
            CustomTemplates[name] = newTemplate;
            Logger.PrintResult($"Template {name} added successfully.");
        }
        else
        {
            Logger.PrintError($"Template {name} is invalid and cannot be added.");
        }
    }

    public static Spaceship? GetSpaceship(string name)
    {
        if (CustomTemplates.TryGetValue(name, out Spaceship? customTemplate))
        {
            return customTemplate;
        }

        return Factories.TryGetValue(name, out ISpaceshipFactory? factory) ? factory.CreateSpaceship() : null;
    }
}
