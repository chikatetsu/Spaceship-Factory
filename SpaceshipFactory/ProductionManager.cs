using SpaceshipFactory.Piece;

namespace SpaceshipFactory;

public static class ProductionManager
{
    private static Dictionary<string, Spaceship> _customTemplates = new();

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
        if (_customTemplates.TryGetValue(type, out Spaceship? customTemplate))
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
            _customTemplates[name] = newTemplate;
            Logger.PrintResult($"Template {name} added successfully.");
        }
        else
        {
            Logger.PrintError($"Template {name} is invalid and cannot be added.");
        }
    }
    
}
