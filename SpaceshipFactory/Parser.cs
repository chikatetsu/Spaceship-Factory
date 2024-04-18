namespace SpaceshipFactory;

public static class Parser
{
    public static void Parse(string input)
    {
        string[] split = input.Split(" ");
        string command = split[0];
        string[] args = split[1..];

        switch (command)
        {
            case "STOCKS":
                if (args.Length > 0)
                {
                    Logger.PrintError("The command STOCKS does not take arguments");
                    break;
                }
                Logger.PrintResult(Stock.GetStocks());
                break;
            case "NEEDED_STOCKS":
                break;
            case "INSTRUCTIONS":
                break;
            case "VERIFY":
                break;
            case "PRODUCE":
                break;
            case "":
                break;
            default:
                Logger.PrintError($"`{command}` is not a known command");
                break;
        }
    }
}
