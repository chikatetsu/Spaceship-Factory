using SpaceshipFactory.Piece;

namespace SpaceshipFactory.Command
{
    public class AddPiecesCommand : IModificationCommand
    {
        private string _spaceshipName;
        private Dictionary<string, int> _modifications;

        public void SetArgs(string spaceshipName, Dictionary<string, int> modifications)
        {
            _spaceshipName = spaceshipName;
            _modifications = modifications;
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
                string pieceName = modification.Key;
                int quantity = modification.Value;

                for (int i = 0; i < quantity; i++)
                {
                    var piece = PieceFactory.CreatePiece(pieceName);
                    if (piece == null)
                    {
                        Logger.PrintError($"Piece '{pieceName}' not recognized.");
                        continue;
                    }

                    if (!spaceship.AddPiece(piece))
                    {
                        Logger.PrintError($"Failed to add piece '{pieceName}' to spaceship '{_spaceshipName}'.");
                    }
                }
            }

            Logger.PrintResult($"Successfully added specified pieces to spaceship '{_spaceshipName}'.");
        }

        public bool Verify(IReadOnlyList<string> args)
        {
            if (args.Count < 3)
            {
                Logger.PrintError("ADD command expects at least a spaceship name and one piece to add.");
                return false;
            }

            if (args[1] != "WITH")
            {
                Logger.PrintError("Expected 'WITH' keyword in ADD command.");
                return false;
            }

            for (int i = 2; i < args.Count; i += 2)
            {
                if (!int.TryParse(args[i], out int quantity) || quantity < 1)
                {
                    Logger.PrintError($"'{args[i]}' is not a valid quantity.");
                    return false;
                }
            }

            return true;
        }
    }
}
