using SpaceshipFactory.Command.Manager;
using SpaceshipFactory.Piece;

namespace SpaceshipFactory.Command
{
    public class ReplacePiecesCommand : IModificationCommand
    {
        private string _spaceshipName;
        private Dictionary<string, (int OldQuantity, string OldPiece, int NewQuantity, string NewPiece)> _modifications;

        public void SetArgs(string spaceshipName, Dictionary<string, (int OldQuantity, string OldPiece, int NewQuantity, string NewPiece)> modifications)
        {
            _spaceshipName = spaceshipName;
            _modifications = modifications;
        }

        public void SetArgs(string spaceshipName, Dictionary<string, int> modifications)
        {
            throw new NotImplementedException();
        }

        public void Execute()
        {
            var spaceship = ProductionManager.GetSpaceship(_spaceshipName);
            if (spaceship == null)
            {
                Logger.PrintError($"Spaceship '{_spaceshipName}' not found.");
                return;
            }

            foreach (var modification in _modifications)
            {
                string oldPieceName = modification.Value.OldPiece;
                int oldQuantity = modification.Value.OldQuantity;
                string newPieceName = modification.Value.NewPiece;
                int newQuantity = modification.Value.NewQuantity;

                for (int i = 0; i < oldQuantity; i++)
                {
                    var piece = spaceship.GetPieceByName(oldPieceName);
                    if (piece == null)
                    {
                        Logger.PrintError($"Old piece '{oldPieceName}' not found in spaceship '{_spaceshipName}'.");
                        continue;
                    }

                    if (!spaceship.RemovePiece(piece))
                    {
                        Logger.PrintError($"Failed to remove old piece '{oldPieceName}' from spaceship '{_spaceshipName}'.");
                    }
                    
                }

                for (int i = 0; i < newQuantity; i++)
                {
                    var newPiece = PieceFactory.CreatePiece(newPieceName);
                    if (newPiece == null)
                    {
                        Logger.PrintError($"New piece '{newPieceName}' not recognized.");
                        continue;
                    }

                    if (!spaceship.AddPiece(newPiece))
                    {
                        Logger.PrintError($"Failed to add new piece '{newPieceName}' to spaceship '{_spaceshipName}'.");
                    }
                }
            }

            Logger.PrintResult($"Successfully replaced specified pieces in spaceship '{_spaceshipName}'.");
        }

        public bool Verify(IReadOnlyList<string> args)
        {
            if (args.Count < 5)
            {
                Logger.PrintError("REPLACE command expects a spaceship name and pairs of old and new pieces with quantities.");
                return false;
            }

            if (args[1] != "REPLACE")
            {
                Logger.PrintError("Expected 'REPLACE' keyword in REPLACE command.");
                return false;
            }

            for (int i = 2; i < args.Count; i += 4)
            {
                if (!int.TryParse(args[i], out int oldQuantity) || oldQuantity < 1)
                {
                    Logger.PrintError($"'{args[i]}' is not a valid quantity for old pieces.");
                    return false;
                }

                if (!int.TryParse(args[i + 2], out int newQuantity) || newQuantity < 1)
                {
                    Logger.PrintError($"'{args[i + 2]}' is not a valid quantity for new pieces.");
                    return false;
                }
            }

            return true;
        }
    }
}
