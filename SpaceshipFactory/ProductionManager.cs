using SpaceshipFactory.Piece;

namespace SpaceshipFactory;

public static class ProductionManager
{
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
}
