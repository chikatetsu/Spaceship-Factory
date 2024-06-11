namespace SpaceshipFactory.Command;

public class VerificationManager: ICommand
{
    private string[]? _args;

    public void Execute()
    {
        if (_args == null)
        {
            return;
        }
        string command = _args[0].ToUpper();
        _args = _args[1..];

        if (!AvailableCommand.Contains(command))
        {
            Logger.PrintError($"`{command}` is not a known command");
            return;
        }

        if (command == "VERIFY" && Verify(_args))
        {
            Execute();
            return;
        }

        if (AvailableCommand.Commands[command].Verify(_args))
        {
            Logger.PrintResult("AVAILABLE");
        }
        else
        {
            Logger.PrintResult("UNAVAILABLE");
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