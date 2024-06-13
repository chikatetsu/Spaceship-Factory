using SpaceshipFactory.Factory;
using SpaceshipFactory.Piece;

namespace SpaceshipFactory.Command;

public class InstructionManager: ICommand
{
    public static readonly List<ISpaceshipFactory> ShipFactories = new()
    {
        new ExplorerFactory(),
        new SpeederFactory(),
        new CargoFactory()
    };
    private Dictionary<Spaceship, uint>? _quantityOfSpaceship;


    public void Execute()
    {
        if (_quantityOfSpaceship != null)
        {
            foreach ((Spaceship model, uint quantity) in _quantityOfSpaceship)
            {
                PrintInstructions(model, quantity);
            }
        }
    }

    public bool Verify(IReadOnlyList<string> args)
    {
        if (args.Count == 0)
        {
            Logger.PrintError("INSTRUCTIONS command expects at least one quantity and one spaceship");
            return false;
        }
        if (args.Count % 2 != 0)
        {
            Logger.PrintError("INSTRUCTIONS command expects an even number of arguments");
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
                    else if (firstLoop)
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
