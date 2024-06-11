using SpaceshipFactory.Piece;

namespace SpaceshipFactory;

public interface ICommand
{
    void Execute();
    bool Verify(IReadOnlyList<string> args);

    protected static Dictionary<Spaceship, uint>? MapArgsToQuantityOfSpaceship(IReadOnlyList<string> args)
    {
        if (args.Count == 0)
        {
            Logger.PrintError("This command expects at least 2 arguments");
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
        return spaceshipQuantities;
    }
}