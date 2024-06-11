using SpaceshipFactory.Piece;

namespace SpaceshipFactory;

public class ProductionManager: ICommand
{
    private static readonly Dictionary<string, Spaceship> CustomTemplates = new();
    private Dictionary<Spaceship, uint>? _quantityOfSpaceship;


    public void Execute()
    {
        foreach ((Spaceship model, uint quantity) in _quantityOfSpaceship)
        {
            Produce(model, quantity);
        }
    }

    public bool Verify(IReadOnlyList<string> args)
    {
        if (args.Count == 0)
        {
            Logger.PrintError("PRODUCE command expects at least 2 argument");
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
        }

        for (var i = 0; i < quantityToProduce; i++)
        {
            Spaceship newSpaceship = new(model.Name);

            foreach ((Piece.Piece piece, uint pieceQuantity) in model.Pieces)
            {
                if (!stock.Remove(piece, pieceQuantity))
                {
                    foreach (var pieceQty in newSpaceship.Pieces)
                    {
                        stock.Add(pieceQty.Key, pieceQty.Value);
                    }
                    Logger.PrintError($"Insufficient stock. Produced {i} {model.Name}");
                    return;
                }

                if (!newSpaceship.Pieces.TryAdd(piece, pieceQuantity))
                {
                    newSpaceship.Pieces[piece] += pieceQuantity;
                }
            }

            stock.Add(newSpaceship, 1);
        }
        Logger.PrintResult("STOCK_UPDATED");
    }
    
    public static Spaceship? CreateSpaceship(string type)
    {
        if (CustomTemplates.TryGetValue(type, out Spaceship? customTemplate))
        {
            return new Spaceship(customTemplate.Name, new Dictionary<Piece.Piece, uint>(customTemplate.Pieces));
        }

        ISpaceshipFactory? factory = type switch
        {
            "Explorer" => new ExplorerFactory(),
            "Speeder" => new SpeederFactory(),
            "Cargo" => new CargoFactory(),
            _ => null
        };

        return factory?.CreateSpaceship();
    }
    
    public static void AddTemplate(string name, Dictionary<Piece.Piece, uint> pieces)
    {
        Spaceship newTemplate = new(name, pieces);
        if (newTemplate.Validate())
        {
            CustomTemplates[name] = newTemplate;
            Logger.PrintResult($"Template {name} added successfully.");
        }
        else
        {
            Logger.PrintError($"Template {name} is invalid and cannot be added.");
        }
    }
    
}
