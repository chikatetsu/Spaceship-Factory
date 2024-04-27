using SpaceshipFactory.Piece;

namespace SpaceshipFactory;

public class Parser
{
    public void Parse(string input)
    {
        string[] split = FormatInput(input);
        string command = split[0];
        string[] args = split[1..];

        switch (command)
        {
            case "STOCKS":
                if (IsStocksCommandValid(args.Length))
                {
                    Logger.PrintResult(Stock.GetStocks());
                }
                break;
            case "NEEDED_STOCKS":
                if (IsNeededStocksCommandValid(args)) { }
                break;
            case "INSTRUCTIONS":
                if (IsInstructionsCommandValid(args)) { }
                break;
            case "VERIFY":
                if (IsVerifyCommandValid(args.Length))
                {
                    VerifyCommand(args);
                }
                break;
            case "PRODUCE":
                if (IsProduceCommandValid(args))
                {
                    ProductionManager.Produce(args);
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
        if (!input.StartsWith(","))
        {
            input = input.Replace(",", "");
        }
        string[] split = input.Split(" ");
        split[0] = split[0].ToUpper();
        return split;
    }


    private void VerifyCommand(string[] args)
    {
        string command = args[0].ToUpper();
        args = args[1..];
        bool isAvailable = false;

        switch (command)
        {
            case "STOCKS":
                if (IsStocksCommandValid(args.Length))
                {
                    Logger.PrintResult("AVAILABLE");
                }
                break;
            case "NEEDED_STOCKS":
                if (IsNeededStocksCommandValid(args))
                {
                    Logger.PrintResult("AVAILABLE");
                }
                break;
            case "INSTRUCTIONS":
                if (IsInstructionsCommandValid(args))
                {
                    Logger.PrintResult("AVAILABLE");
                }
                break;
            case "VERIFY":
                if (IsVerifyCommandValid(args.Length))
                {
                    VerifyCommand(args);
                }
                break;
            case "PRODUCE":
                if (IsProduceCommandValid(args))
                {
                    for (int i = 0; i < args.Length; i += 2)
                    {
                        if (!int.TryParse(args[i], out int quantity) || quantity < 1)
                        {
                            continue;
                        }
                        Spaceship spaceship = new Spaceship(args[i + 1]);

                        if (!Stock.Verify(spaceship, quantity))
                        {
                            Logger.PrintResult("UNAVAILABLE");
                            return;
                        }
                    }
                    Logger.PrintResult("AVAILABLE");
                }
                break;
            default:
                Logger.PrintError($"`{command}` is not a known command");
                break;
        }
    }


    private bool IsStocksCommandValid(int argsLength)
    {
        if (argsLength != 0)
        {
            Logger.PrintError("STOCKS command does not expect any arguments");
            return false;
        }
        return true;
    }


    private bool IsNeededStocksCommandValid(IReadOnlyList<string> args)
    {
        if (args.Count == 0)
        {
            Logger.PrintError("NEEDED_STOCKS command expects at least 2 argument");
            return false;
        }
        if (args.Count % 2 != 0)
        {
            Logger.PrintError("NEEDED_STOCKS command expects an even number of arguments");
            return false;
        }
        for (int i = 0; i < args.Count; i += 2)
        {
            if (!int.TryParse(args[i], out _))
            {
                Logger.PrintError($"`{args[i]}` is not a valid quantity");
                return false;
            }
        }
        return true;
    }


    private bool IsInstructionsCommandValid(IReadOnlyList<string> args)
    {
        if (args.Count == 0)
        {
            Logger.PrintError("INSTRUCTIONS command expects at least 2 argument");
            return false;
        }
        if (args.Count % 2 != 0)
        {
            Logger.PrintError("INSTRUCTIONS command expects an even number of arguments");
            return false;
        }
        for (int i = 0; i < args.Count; i += 2)
        {
            if (!int.TryParse(args[i], out _))
            {
                Logger.PrintError($"`{args[i]}` is not a valid quantity");
                return false;
            }
        }
        return true;
    }


    private bool IsVerifyCommandValid(int argsLength)
    {
        if (argsLength < 1)
        {
            Logger.PrintError("VERIFY command expects at least one argument");
            return false;
        }
        return true;
    }


    private bool IsProduceCommandValid(IReadOnlyList<string> args)
    {
        if (args.Count == 0)
        {
            Logger.PrintError("PRODUCE command expects at least 2 argument");
            return false;
        }
        if (args.Count % 2 != 0)
        {
            Logger.PrintError("PRODUCE command expects an even number of arguments");
            return false;
        }
        for (int i = 0; i < args.Count; i += 2)
        {
            if (!int.TryParse(args[i], out _))
            {
                Logger.PrintError($"`{args[i]}` is not a valid quantity");
                return false;
            }
        }
        return true;
    }
}
