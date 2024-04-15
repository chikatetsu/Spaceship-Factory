namespace SpaceshipFactory;

public static class Program
{
	public static void Main()
	{
		Logger.InitCommandColor();
		string input = Console.ReadLine() ?? string.Empty;
		while (input != "EXIT")
		{
			Parser.Parse(input);
			input = Console.ReadLine() ?? string.Empty;
		}
	}
}
