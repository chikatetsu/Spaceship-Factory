namespace SpaceshipFactory;

public static class Logger
{
    public static void InitCommandColor()
    {
        Console.ForegroundColor = ConsoleColor.Green;
    }

    public static void PrintInstruction(string command, string args)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"{command} ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(args);
        InitCommandColor();
    }

    public static void PrintResult(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        InitCommandColor();
    }

    public static void PrintError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("ERROR ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(message);
        InitCommandColor();
    }

    public static void PrintDebug(string s)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(s);
        InitCommandColor();
    }
}
