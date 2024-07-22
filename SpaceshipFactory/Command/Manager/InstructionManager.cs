using SpaceshipFactory.Factory;
using SpaceshipFactory.Piece;

namespace SpaceshipFactory.Command.Manager;

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

            if (spaceship.Hull != null)
            {
                Logger.PrintInstruction("GET_OUT_OF_STOCK", $"1 {spaceship.Hull.Name}");
            }

            var enginesQuantities = spaceship.Engines.GroupBy(obj => obj)
                .Select(g => new { Engine = g.Key, Quantite = g.Count() });
            foreach (var engine in enginesQuantities)
            {
                Logger.PrintInstruction("GET_OUT_OF_STOCK", $"{engine.Quantite} {engine.Engine}");
            }

            var wingsQuantities = spaceship.Wings.GroupBy(obj => obj)
                .Select(g => new { Wing = g.Key, Quantite = g.Count() });
            foreach (var wings in wingsQuantities)
            {
                Logger.PrintInstruction("GET_OUT_OF_STOCK", $"{wings.Quantite} {wings.Wing}");
            }

            var thursterQuantities = spaceship.Thrusters.GroupBy(obj => obj)
                .Select(g => new { Thurster = g.Key, Quantite = g.Count() });
            foreach (var thruster in thursterQuantities)
            {
                Logger.PrintInstruction("GET_OUT_OF_STOCK", $"{thruster.Quantite} {thruster.Thurster}");
            }

            bool firstLoop = true;
            string currentAssembly = "";
            
            if (spaceship.Hull != null)
            {
                currentAssembly = spaceship.Hull.Name;
            }

            foreach (var engine in spaceship.Engines)
            {
                switch (firstLoop)
                {
                    case true when string.IsNullOrEmpty(currentAssembly):
                        currentAssembly = engine.Name;
                        break;
                    case true:
                        Logger.PrintInstruction("ASSEMBLE", $"{currentAssembly} + {engine.Name}");
                        currentAssembly += $", {engine.Name}";
                        firstLoop = false;
                        break;
                    default:
                        Logger.PrintInstruction("ASSEMBLE", $"[{currentAssembly}] + {engine.Name}");
                        currentAssembly += $", {engine.Name}";
                        break;
                }
            }

            foreach (var wings in spaceship.Wings)
            {
                if (firstLoop && string.IsNullOrEmpty(currentAssembly))
                {
                    currentAssembly = wings.Name;
                }
                else if (firstLoop)
                {
                    Logger.PrintInstruction("ASSEMBLE", $"{currentAssembly} + {wings.Name}");
                    currentAssembly += $", {wings.Name}";
                    firstLoop = false;
                }
                else
                {
                    Logger.PrintInstruction("ASSEMBLE", $"[{currentAssembly}] + {wings.Name}");
                    currentAssembly += $", {wings.Name}";
                }
            }

            foreach (var thruster in spaceship.Thrusters)
            {
                if (firstLoop && string.IsNullOrEmpty(currentAssembly))
                {
                    currentAssembly = thruster.Name;
                }
                else if (firstLoop)
                {
                    Logger.PrintInstruction("ASSEMBLE", $"{currentAssembly} + {thruster.Name}");
                    currentAssembly += $", {thruster.Name}";
                    firstLoop = false;
                }
                else
                {
                    Logger.PrintInstruction("ASSEMBLE", $"[{currentAssembly}] + {thruster.Name}");
                    currentAssembly += $", {thruster.Name}";
                }
            }

            Logger.PrintInstruction("FINISHED", spaceship.Name);
        }
    }
}
