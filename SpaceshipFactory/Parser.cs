namespace SpaceshipFactory;

public class Parser
{
    public void Parse(string input)
    {
        string[] split = FormatInput(input);
        if (split.Length == 0)
        {
            Logger.PrintError("No command entered.");
            return;
        }

        string command = split[0];
        string[] args = split[1..];
        Logger.PrintDebug($"Command: {command}, Args: {string.Join(" ", args)}");
        ICommand commandExecutor;

        switch (command)
        {
            case "STOCKS":
                commandExecutor = new StockManager();
                if (commandExecutor.Verify(args))
                {
                    commandExecutor.Execute();
                }
                break;
            case "NEEDED_STOCKS":
                commandExecutor = new StockCalculator();
                if (commandExecutor.Verify(args))
                {
                    commandExecutor.Execute();
                }
                break;
            case "INSTRUCTIONS":
                commandExecutor = new InstructionManager();
                if (commandExecutor.Verify(args))
                {
                    commandExecutor.Execute();
                }
                break;
            case "VERIFY":
                commandExecutor = new VerificationManager();
                if (commandExecutor.Verify(args))
                {
                    commandExecutor.Execute();
                }
                break;
            case "PRODUCE":
                commandExecutor = new ProductionManager();
                if (commandExecutor.Verify(args))
                {
                    commandExecutor.Execute();
                }
                break;
            case "ADD_TEMPLATE":
                commandExecutor = new TemplateManager();
                if (commandExecutor.Verify(args))
                {
                    commandExecutor.Execute();
                }
                break;
            case "":
                break;
            default:
                Logger.PrintError($"`{command}` is not a known command");
                break;
        }
    }
    
    private string[] FormatInput(string input)
    {
        input = input.Trim().Replace("  ", " ");
        string[] split = input.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);

        if (split.Length == 0)
        {
            return Array.Empty<string>();
        }

        string command = split[0].ToUpper();
        string[] args = split.Length > 1 ? split[1].Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries) : Array.Empty<string>();

        List<string> allArgs = new();
        foreach (var arg in args)
        {
            allArgs.AddRange(arg.Split(' ', StringSplitOptions.RemoveEmptyEntries));
        }

        string[] result = new string[allArgs.Count + 1];
        result[0] = command;
        allArgs.CopyTo(result, 1);

        Logger.PrintDebug($"Formatted Input: Command - {command}, Args - {string.Join(" ", allArgs)}");

        return result;
    }
}
