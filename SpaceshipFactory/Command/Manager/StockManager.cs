namespace SpaceshipFactory.Command;

public class StockManager: ICommand
{
    public void Execute()
    {
        Logger.PrintResult(Stock.Instance.GetStocks());
    }

    public bool Verify(IReadOnlyList<string> args)
    {
        if (args.Count == 0) return true;
        Logger.PrintError("STOCKS command does not expect any arguments");
        return false;
    }
}