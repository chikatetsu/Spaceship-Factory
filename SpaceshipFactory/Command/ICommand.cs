using System.Collections.Generic;
using SpaceshipFactory.Piece;

namespace SpaceshipFactory.Command;

public interface ICommand
{
    void Execute();
    bool Verify(IReadOnlyList<string> args);

    protected static Dictionary<Spaceship, uint>? MapArgsToQuantityOfSpaceship(IReadOnlyList<string> args)
    {
        if (args.Count == 0)
        {
            Logger.PrintError("This command expects at least one quantity and one spaceship");
            return null;
        }
        if (args.Count % 2 != 0)
        {
            Logger.PrintError("This command expects an even number of arguments");
            return null;
        }

        var spaceshipQuantities = new Dictionary<Spaceship, uint>();
        for (int i = 0; i < args.Count; i += 2)
        {
            if (!uint.TryParse(args[i], out uint quantity) || quantity < 1)
            {
                Logger.PrintError($"`{args[i]}` is not a valid quantity");
                return null;
            }

            string modelName = args[i + 1];

            Spaceship? model = ProductionManager.CreateSpaceship(modelName);
            if (model == null)
            {
                Logger.PrintError($"Spaceship model '{modelName}' is not available.");
                continue;
            }

            if (!spaceshipQuantities.TryAdd(model, quantity))
            {
                spaceshipQuantities[model] += quantity;
            }
        }

        if (spaceshipQuantities.Count == 0)
        {
            return null;
        }
        return spaceshipQuantities;
    }

    protected static Dictionary<Spaceship, AddedStockInfo>? MapArgsToQuantityOfAddedSpaceshipToStock(IReadOnlyList<string> args)
    {
        if (args.Count == 0)
        {
            Logger.PrintError("This command expects at least one quantity and one spaceship");
            return null;
        }
        if (args.Count % 3 != 0)
        {
            Logger.PrintError("This command expects an even number of arguments");
            return null;
        }

        var spaceshipQuantities =  new Dictionary<Spaceship, AddedStockInfo>();
        for (int i = 0; i < args.Count; i += 3)
        {

            string addedType = args[i];
            string modelName = args[i + 2];

            if (addedType != "S" && addedType != "P" && addedType != "A")
            {
                Logger.PrintError($"`{args[i]}` is not a valid type, must either be, P for piece, A for assembly or S for spaceship");
                return null;
            }

            


            if (!uint.TryParse(args[i + 1], out uint quantity) || quantity < 1)
            {
                Logger.PrintError($"`{args[i + 1]}` is not a valid quantity");
                return null;
            }

            if (addedType == "S")
            {

                Spaceship? model = ProductionManager.CreateSpaceship(modelName);
                if (model == null)
                {
                    Logger.PrintError($"Spaceship model '{modelName}' is not available.");
                    continue;
                }

                if (!spaceshipQuantities.TryAdd(model, new AddedStockInfo(quantity, addedType)))
                {
                    spaceshipQuantities[model].Quantity += quantity;
                }
            }



        }

        if (spaceshipQuantities.Count == 0)
        {
            return null;
        }
        return spaceshipQuantities;
    }

    protected static Dictionary<Piece.Piece, AddedStockInfo>? MapArgsToQuantityOfAddedPieceToStock(IReadOnlyList<string> args)
    {
        if (args.Count == 0)
        {
            Logger.PrintError("This command expects at least one quantity and one spaceship");
            return null;
        }
        if (args.Count % 3 != 0)
        {
            Logger.PrintError("This command expects an even number of arguments");
            return null;
        }

        var pieceQuantities = new Dictionary<Piece.Piece, AddedStockInfo>();
        for (int i = 0; i < args.Count; i += 3)
        {

            string addedType = args[i];
            string modelName = args[i + 2];

            if (addedType != "S" && addedType != "P" && addedType != "A")
            {
                Logger.PrintError($"`{args[i]}` is not a valid type, must either be, P for piece, A for assembly or S for spaceship");
                return null;
            }


            if (!uint.TryParse(args[i + 1], out uint quantity) || quantity < 1)
            {
                Logger.PrintError($"`{args[i + 1]}` is not a valid quantity");
                return null;
            }

            if (addedType == "P")
            {

                Piece.Piece newPiece = PieceFactory.CreatePiece(modelName);
                if (newPiece == null)
                {
                    Logger.PrintError($"Piece '{modelName}' not recognized.");
                    continue;
                }

                if (!pieceQuantities.TryAdd(newPiece, new AddedStockInfo(quantity, addedType)))
                {
                    pieceQuantities[newPiece].Quantity += quantity;
                }
            }


        }

        if (pieceQuantities.Count == 0)
        {
            return null;
        }
        return pieceQuantities;
    }

}