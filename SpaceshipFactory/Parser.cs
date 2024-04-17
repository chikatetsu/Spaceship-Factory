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
                break;
            case "NEEDED_STOCKS":
                break;
            case "INSTRUCTIONS":
                break;
            case "VERIFY":
                break;
            case "PRODUCE":
                ProductionManager.Produce(args);
                break;
            case "":
                break;
            default:
                Logger.PrintError($"`{command}` is not a known command");
                break;
        }
    }
}
