using SpaceshipFactory.Command.Manager;
using SpaceshipFactory.Piece;

namespace SpaceshipFactory.Command;

public class TemplateManager: ICommand
{
    private string? _templateName;
    private List<Piece.Piece>? _pieces;

    public void Execute()
    {
        if (_templateName != null && _pieces != null)
        {
            ProductionManager.AddTemplate(_templateName, _pieces);
        }
    }

    public bool Verify(IReadOnlyList<string> args)
    {
        if (args.Count < 2)
        {
            Logger.PrintError("ADD_TEMPLATE command expects a template name and pieces");
            return false;
        }

        _templateName = args[0];
        _pieces = new List<Piece.Piece>();

        for (int i = 1; i < args.Count; i++)
        {
            Piece.Piece? piece = PieceFactory.CreatePiece(args[i]);
            if (piece == null)
            {
                Logger.PrintError($"Unknown piece type: {args[i]}");
                return false;
            }

            _pieces.Add(piece);
        }

        return true;
    }
}