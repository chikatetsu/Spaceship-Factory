namespace SpaceshipFactory;

public static class Program
{
	public static void Main()
	{
		Logger.InitCommandColor();
		Parser parser = new();
		string input = Console.ReadLine() ?? string.Empty;
		while (input != "EXIT")
		{
			parser.Parse(input);
			input = Console.ReadLine() ?? string.Empty;
		}
	}
}
