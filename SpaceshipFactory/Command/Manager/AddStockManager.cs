using SpaceshipFactory.Piece;

namespace SpaceshipFactory.Command;

public class AddStockManager: ICommand
{

    private Dictionary<Piece.Piece, AddedStockInfo>? _quantityOfPiece;
    private Dictionary<Spaceship, AddedStockInfo>? _quantityOfSpaceship;


    public void Execute()
    {
        var stock = Stock.Instance;
        
        if (_quantityOfSpaceship != null)
        {
            foreach ((Spaceship model, AddedStockInfo addedStockInfo) in _quantityOfSpaceship)
            {
                stock.Add(model, addedStockInfo.Quantity);
            }
        }


        if (_quantityOfPiece != null)
        {
            foreach ((Piece.Piece piece, AddedStockInfo addedStockInfo) in _quantityOfPiece)
            {
                stock.Add(piece, addedStockInfo.Quantity);
            }
        }

        Logger.PrintResult("Nouveaux Stocks :");
        Logger.PrintResult(Stock.Instance.GetStocks());
    }

    public bool Verify(IReadOnlyList<string> args)
    {
        if (args.Count == 0)
        {
            Logger.PrintError("RECEIVE command expects at least one quantity and one piece, assembly or spaceship");
            return false;
        }
        if (args.Count % 3 != 0)
        {
            Logger.PrintError("RECEIVE command expects arguments that comes by 3");
            return false;
        }
        for (int i = 0; i < args.Count; i += 3)
        {
            if (!int.TryParse(args[i+1], out int quantity) || quantity < 1)
            {
                Logger.PrintError($"`{args[i]}` is not a valid quantity");
                return false;
            }
        }

        _quantityOfSpaceship = ICommand.MapArgsToQuantityOfAddedSpaceshipToStock(args);
        _quantityOfPiece = ICommand.MapArgsToQuantityOfAddedPieceToStock(args);
        return (_quantityOfSpaceship != null) && (_quantityOfPiece != null);
    }
}