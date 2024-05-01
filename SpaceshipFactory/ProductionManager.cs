using SpaceshipFactory.Piece;

namespace SpaceshipFactory;

public static class ProductionManager
{
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

            Spaceship? spaceship = InstructionManager.ShipModels.Find(spaceship => spaceship.Name == modelArg);
            if (spaceship == null)
            {
                Logger.PrintError($"Spaceship model '{modelArg}' is not available.");
                continue;
            }

            if(isInstructions)
            {
                Instructions(spaceship, quantity);
            }
            else
            {
                Assemble(spaceship, quantity);
            }
        }
    }


    private static void Assemble(Spaceship spaceship, int spaceshipQuantity)
    {
        if (!Stock.Verify(spaceship, spaceshipQuantity))
        {
            Logger.PrintError("Unable to start production due to insufficient stock.");
        }

        for (int i = 0; i < spaceshipQuantity; i++)
        {
            Spaceship newSpaceship = new(spaceship.Name);

            foreach ((Piece.Piece piece, uint pieceQuantity) in spaceship.Pieces)
            {
                if (!Stock.Remove(piece, pieceQuantity))
                {
                    foreach (var pieceQty in newSpaceship.Pieces)
                    {
                        Stock.Add(pieceQty.Key, pieceQty.Value);
                    }
                    Logger.PrintError($"Insufficient stock. Produced {i} {spaceship.Name}");
                    return;
                }

                if (!newSpaceship.Pieces.TryAdd(piece, pieceQuantity))
                {
                    newSpaceship.Pieces[piece] += pieceQuantity;
                }
            }

            Stock.Add(newSpaceship, 1);
        }
        Logger.PrintResult("STOCK_UPDATED");
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

            Logger.PrintInstruction("FINISHED", spaceship.Name);
        }
    }
}
