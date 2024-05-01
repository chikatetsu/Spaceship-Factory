using SpaceshipFactory.Piece;

namespace SpaceshipFactory;

public static class ProductionManager
{
    private static readonly List<Spaceship> ShipModels = new()
    {
        new Spaceship("Explorer", new Dictionary<Piece.Piece, uint>
        {
            { new Hull("Hull_HE1"), 1 },
            { new Engine("Engine_EE1"), 1 },
            { new Wings("Wings_WE1"), 1 },
            { new Thruster("Thruster_TE1"), 1 }
        }),
        new Spaceship("Speeder", new Dictionary<Piece.Piece, uint>
        {
            { new Hull("Hull_HS1"), 1 },
            { new Engine("Engine_ES1"), 1 },
            { new Wings("Wings_WS1"), 1 },
            { new Thruster("Thruster_TS1"), 2 }
        }),
        new Spaceship("Cargo", new Dictionary<Piece.Piece, uint>
        {
            { new Hull("Hull_HC1"), 1 },
            { new Engine("Engine_EC1"), 1 },
            { new Wings("Wings_WC1"), 1 },
            { new Thruster("Thruster_TC1"), 1 }
        })
    };


    public static void Produce(string[] args, bool isInstructions)
    {
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

            if(isInstructions) Instructions(spaceship, quantity);
            else Assemble(spaceship, quantity);
        }
    }


    private static void Assemble(Spaceship spaceship, int quantity)
    {
        if (!Stock.Verify(spaceship, quantity))
        {
            Logger.PrintError("Unable to start production due to insufficient stock.");
        }

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
            /*
            foreach (var PieceFromStock in piecesFromStock)
            {
                Logger.PrintInstruction("ASSEMBLE", $"{PieceFromStock.Key.Name}");
            }
            */

            Logger.PrintResult($"FINISHED {spaceship.Name}");
        }
    }

    private static void Instructions(Spaceship spaceship, int quantity)
    {
        if (!Stock.Verify(spaceship, quantity))
        {
            Logger.PrintError("Unable to start production due to insufficient stock.");
        }

        for (int i = 0; i < quantity; i++)
        {
            Logger.PrintInstruction("PRODUCING", $"{spaceship.Name}");

            foreach (var piece in spaceship.Pieces)
            {
                Logger.PrintInstruction("GET_OUT_OF_STOCK", $"{piece.Value} {piece.Key.Name}");
            }

            bool firstLoop = true;
            string currentAssembly = "";

            foreach (var piece in spaceship.Pieces)
            {
                if (currentAssembly == "")
                {
                    //Logger.PrintInstruction("ASSEMBLE", $"{piece.Key.Name}");
                    currentAssembly = $"{piece.Key.Name}";
                    firstLoop = false;
                }
                else
                {
                    if (firstLoop)
                    {
                        Logger.PrintInstruction("ASSEMBLE", $"{currentAssembly} {piece.Key.Name}");
                        currentAssembly = $"{currentAssembly}, {piece.Key.Name}";
                        firstLoop = false;
                    }
                    else
                    {
                        Logger.PrintInstruction("ASSEMBLE", $"[{currentAssembly}] {piece.Key.Name}");
                        currentAssembly = $"{currentAssembly}, {piece.Key.Name}";
                    }
                }
            }

            Logger.PrintResult($"FINISHED {spaceship.Name}");
        }
    }
}
