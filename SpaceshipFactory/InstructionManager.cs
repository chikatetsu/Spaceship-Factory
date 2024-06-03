using SpaceshipFactory.Piece;

namespace SpaceshipFactory;

public class InstructionManager
{
    public static readonly List<ISpaceshipFactory> ShipFactories = new()
    {
        new ExplorerFactory(),
        new SpeederFactory(),
        new CargoFactory()
    };


    public static void PrintInstructions(Spaceship spaceship, uint quantity)
    {
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
                for(int pieceQuantity = 0; pieceQuantity < piece.Value; pieceQuantity++)
                {
                    if (currentAssembly == "")
                    {
                        //Logger.PrintInstruction("ASSEMBLE", $"{piece.Key.Name}");
                        currentAssembly = $"{piece.Key.Name}";
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
                
            }

            Logger.PrintInstruction("FINISHED", spaceship.Name);
        }
    }
}
