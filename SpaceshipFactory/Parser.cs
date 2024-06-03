using SpaceshipFactory.Piece;

namespace SpaceshipFactory;

public class Parser
{
    public void Parse(string input)
    {
        string[] split = FormatInput(input);
        if (split.Length == 0)
        {
            Logger.PrintError("No command entered.");
            return;
        }

        string command = split[0];
        string[] args = split[1..];
        Dictionary<Spaceship, uint>? modelQuantity;
        Logger.PrintDebug($"Command: {command}, Args: {string.Join(" ", args)}");

        switch (command)
        {
            case "STOCKS":
                if (IsStocksCommandValid(args.Length))
                {
                    Logger.PrintResult(Stock.Instance.GetStocks());
                }
                break;
            case "NEEDED_STOCKS":
                modelQuantity = MapArgsToQuantityOfSpaceship(args);
                if (modelQuantity != null)
                {
                    StockCalculator.PrintNeededStocks(modelQuantity);
                }
                break;
            case "INSTRUCTIONS":
                modelQuantity = MapArgsToQuantityOfSpaceship(args);
                if (modelQuantity != null)
                {
                    foreach ((Spaceship model, uint quantity) in modelQuantity)
                    {
                        InstructionManager.PrintInstructions(model, quantity);
                    }
                }
                break;
            case "VERIFY":
                if (IsVerifyCommandValid(args.Length))
                {
                    VerifyCommand(args);
                }
                break;
            case "PRODUCE":
                modelQuantity = MapArgsToQuantityOfSpaceship(args);
                if (modelQuantity != null)
                {
                    foreach ((Spaceship model, uint quantity) in modelQuantity)
                    {
                        ProductionManager.Produce(model, quantity);
                    }
                }
                break;
            case "ADD_TEMPLATE":
                AddTemplateCommand(args);
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
        string[] split;

        split = input.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);

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
                    var stock = Stock.Instance;
                    for (int i = 0; i < args.Length; i += 2)
                    {
                        if (!uint.TryParse(args[i], out uint quantity) || quantity < 1)
                        {
                            continue;
                        }
                        Spaceship spaceship = new Spaceship(args[i + 1]);

                        if (stock.IsStockSufficient(spaceship, quantity)) continue;
                        Logger.PrintResult("UNAVAILABLE");
                        return;
                    }
                    Logger.PrintResult("AVAILABLE");
                }
                break;
            default:
                Logger.PrintError($"`{command}` is not a known command");
                break;
        }
    }
    
    
    private static void AddTemplateCommand(string[] args)
    {
        if (args.Length < 2)
        {
            Logger.PrintError("Invalid ADD_TEMPLATE command arguments");
            return;
        }

        string templateName = args[0];
        var pieces = new Dictionary<Piece.Piece, uint>();
    
        for (int i = 1; i < args.Length; i++)
        {
            Piece.Piece? piece = PieceFactory.CreatePiece(args[i]);
            if (piece == null)
            {
                Logger.PrintError($"Unknown piece type: {args[i]}");
                return;
            }

            if (!pieces.TryAdd(piece, 1))
            {
                pieces[piece] += 1;
            }
        }

        ProductionManager.AddTemplate(templateName, pieces);
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
            if (!int.TryParse(args[i], out int quantity) || quantity < 1)
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
            if (!int.TryParse(args[i], out int quantity) || quantity < 1)
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
            if (!int.TryParse(args[i], out int quantity) || quantity < 1)
            {
                Logger.PrintError($"`{args[i]}` is not a valid quantity");
                return false;
            }
        }
        return true;
    }


    private static Dictionary<Spaceship, uint>? MapArgsToQuantityOfSpaceship(IReadOnlyList<string> args)
    {
        if (args.Count == 0)
        {
            Logger.PrintError("PRODUCE command expects at least 2 argument");
            return null;
        }
        if (args.Count % 2 != 0)
        {
            Logger.PrintError("PRODUCE command expects an even number of arguments");
            return null;
        }

        var spaceshipQuantities = new Dictionary<Spaceship, uint>();
        for (int i = 0; i < args.Count; i += 2)
        {
            if (!uint.TryParse(args[i], out uint quantity) || quantity < 1)
            {
                Logger.PrintError($"`{args[i]}` is not a valid quantity");
                return null;
            }

            string modelName = args[i + 1];

            Spaceship? model = ProductionManager.CreateSpaceship(modelName);
            if (model == null)
            {
                Logger.PrintError($"Spaceship model '{modelName}' is not available.");
                continue;
            }

            if (!spaceshipQuantities.TryAdd(model, quantity))
            {
                spaceshipQuantities[model] += quantity;
            }
        }
        return spaceshipQuantities;
    }
}
