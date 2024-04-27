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
                if (IsVerifyCommandValid(args.Length)) { }
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
        if (!input.StartsWith(","))
        {
            input = input.Replace(",", "");
        }
        string[] split = input.Split(" ");
        split[0] = split[0].ToUpper();
        return split;
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

    //Si la commande est incorrecte, le résultat sera affiché sous la forme :
    // ERROR Message
    // Où Message indique pourquoi la commande est incorrecte
    // Si la commande est valide et que le stock est suffisant, le résultat sera :
    // AVAILABLE
    // Si la commande est valide mais que le stock n’est pas suffisant, le résultat sera :
    // UNAVAILABLE
}
