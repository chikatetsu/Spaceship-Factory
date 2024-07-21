using SpaceshipFactory.Command.Manager;

namespace SpaceshipFactory.Command;

public static class AvailableCommand
{
    public static readonly Dictionary<string, ICommand> Commands = new()
    {
        { "STOCKS", new StockManager() },
        { "NEEDED_STOCKS", new StockCalculator() },
        { "INSTRUCTIONS", new InstructionManager() },
        { "VERIFY", new VerificationManager() },
        { "PRODUCE", new ProductionManager() },
        { "ADD_TEMPLATE", new TemplateManager() },
        { "MODIFY", new ModificationManager() },
        { "RECEIVE", new AddStockManager() }
    };

    public static bool Contains(string commandName)
    {
        return Commands.ContainsKey(commandName);
    }
}