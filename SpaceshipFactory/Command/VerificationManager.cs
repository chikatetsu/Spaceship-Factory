namespace SpaceshipFactory;

public class VerificationManager: ICommand
{
    private string[] _args;

    public void Execute()
    {
        string command = _args[0].ToUpper();
        _args = _args[1..];
        ICommand commandExecutor;

        switch (command)
        {
            case "STOCKS":
                commandExecutor = new StockManager();
                if (commandExecutor.Verify(_args))
                {
                    Logger.PrintResult("AVAILABLE");
                }
                else
                {
                    Logger.PrintResult("UNAVAILABLE");
                }
                break;
            case "NEEDED_STOCKS":
                commandExecutor = new StockCalculator();
                if (commandExecutor.Verify(_args))
                {
                    Logger.PrintResult("AVAILABLE");
                }
                else
                {
                    Logger.PrintResult("UNAVAILABLE");
                }
                break;
            case "INSTRUCTIONS":
                commandExecutor = new InstructionManager();
                if (commandExecutor.Verify(_args))
                {
                    Logger.PrintResult("AVAILABLE");
                }
                else
                {
                    Logger.PrintResult("UNAVAILABLE");
                }
                break;
            case "VERIFY":
                if (Verify(_args))
                {
                    Execute();
                }
                break;
            case "PRODUCE":
                commandExecutor = new ProductionManager();
                if (commandExecutor.Verify(_args))
                {
                    Logger.PrintResult("AVAILABLE");
                }
                else
                {
                    Logger.PrintResult("UNAVAILABLE");
                }
                break;
            case "ADD_TEMPLATE":
                commandExecutor = new InstructionManager();
                if (commandExecutor.Verify(_args))
                {
                    Logger.PrintResult("AVAILABLE");
                }
                else
                {
                    Logger.PrintResult("UNAVAILABLE");
                }
                break;
            default:
                Logger.PrintError($"`{command}` is not a known command");
                break;
        }
    }

    public bool Verify(IReadOnlyList<string> args)
    {
        if (args.Count < 1)
        {
            Logger.PrintError("VERIFY command expects at least one argument");
            return false;
        }
        _args = args as string[];
        return true;
    }
}