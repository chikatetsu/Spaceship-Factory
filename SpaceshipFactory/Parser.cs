using SpaceshipFactory.Command;

namespace SpaceshipFactory;

public static class Parser
{
    public static void Parse(string input)
    {
        string[] split = FormatInput(input);
        if (split.Length == 0)
        {
            Logger.PrintError("No command entered.");
            return;
        }

        string command = split[0];
        if (command == "")
        {
            return;
        }
        if (!AvailableCommand.Contains(command))
        {
            Logger.PrintError($"`{command}` is not a known command");
            return;
        }
        string[] args = split[1..];
        Logger.PrintDebug($"Command: {command}, Args: {string.Join(" ", args)}");
        
        if (AvailableCommand.Commands[command].Verify(args))
        {
            AvailableCommand.Commands[command].Execute();
        }
    }
    
    private static string[] FormatInput(string input)
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
