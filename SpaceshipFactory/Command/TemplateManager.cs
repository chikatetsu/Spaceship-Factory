using SpaceshipFactory.Piece;

namespace SpaceshipFactory;

public class TemplateManager: ICommand
{
    private string TemplateName;
    private Dictionary<Piece.Piece, uint> Pieces;

    public void Execute()
    {
        ProductionManager.AddTemplate(TemplateName, Pieces);
    }

    public bool Verify(IReadOnlyList<string> args)
    {
        if (args.Count < 2)
        {
            Logger.PrintError("Invalid ADD_TEMPLATE command arguments");
            return false;
        }

        TemplateName = args[0];
        Pieces = new Dictionary<Piece.Piece, uint>();

        for (int i = 1; i < args.Count; i++)
        {
            Piece.Piece? piece = PieceFactory.CreatePiece(args[i]);
            if (piece == null)
            {
                Logger.PrintError($"Unknown piece type: {args[i]}");
                return false;
            }

            if (!Pieces.TryAdd(piece, 1))
            {
                Pieces[piece] += 1;
            }
        }

        return true;
    }
}