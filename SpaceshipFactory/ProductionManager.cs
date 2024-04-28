using SpaceshipFactory.Piece;

namespace SpaceshipFactory;

public static class ProductionManager
{
    private static readonly List<Spaceship?> ShipModels = new()
    {
        new Spaceship("Explorer"),
        new Spaceship("Speeder"),
        new Spaceship("Cargo")
    };


    public static void Produce(string[] args)
    {
        ShipModels[0]?.AddPiece(new Hull("Hull_HE1"), 1);
        ShipModels[0]?.AddPiece(new Engine("Engine_EE1"), 1);
        ShipModels[0]?.AddPiece(new Wings("Wings_WE1"), 1);
        ShipModels[0]?.AddPiece(new Thruster("Thruster_TE1"), 1);

        ShipModels[1]?.AddPiece(new Hull("Hull_HS1"), 1);
        ShipModels[1]?.AddPiece(new Engine("Engine_ES1"), 1);
        ShipModels[1]?.AddPiece(new Wings("Wings_WS1"), 1);
        ShipModels[1]?.AddPiece(new Thruster("Thruster_TS1"), 2);

        ShipModels[2]?.AddPiece(new Hull("Hull_HC1"), 1);
        ShipModels[2]?.AddPiece(new Engine("Engine_EC1"), 1);
        ShipModels[2]?.AddPiece(new Wings("Wings_WC1"), 1);
        ShipModels[2]?.AddPiece(new Thruster("Thruster_TC1"), 1);

        for (int i = 0; i < args.Length; i += 2)
        {
            string quantityArg = args[i];
            string modelArg = args[i + 1];

            if (!int.TryParse(quantityArg, out int quantity) || quantity < 1)
            {
                continue;
            }

            Spaceship? spaceship = ShipModels.Find(spaceship => spaceship.Name == modelArg);

            if (spaceship == null)
            {
                Logger.PrintError($"Spaceship model '{modelArg}' is not available.");
                continue;
            }

            Assemble(spaceship, quantity);
        }
    }


    private static void Assemble(Spaceship spaceship, int quantity)
    {
        if (Stock.Verify(spaceship, quantity))
        {
            for (int i = 0; i < quantity; i++)
            {
                Logger.PrintInstruction("PRODUCING", $"{spaceship.Name}");
                Dictionary<Piece.Piece, uint> piecesFromStock = new();

                foreach (var piece in spaceship.Pieces)
                {
                    if (!Stock.Remove(piece.Key, piece.Value))
                    {
                        foreach (var pieceQty in piecesFromStock)
                        {
                            Stock.Add(pieceQty.Key, pieceQty.Value);
                        }

                        return;
                    }

                    if (piecesFromStock.ContainsKey(piece.Key))
                    {
                        piecesFromStock[piece.Key]+= piece.Value;
                    }
                    else
                    {
                        piecesFromStock.Add(piece.Key, piece.Value);
                    }
                }

                foreach (var PieceFromStock in piecesFromStock)
                {
                    Logger.PrintInstruction("ASSEMBLE", $"{PieceFromStock.Key.Name}");
                }

                Logger.PrintResult($"FINISHED {spaceship.Name}");
            }
        }
        else
        {
            Logger.PrintError("Unable to start production due to insufficient stock.");
        }
    }
}
