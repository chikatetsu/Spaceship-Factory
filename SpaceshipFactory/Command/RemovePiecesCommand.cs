using SpaceshipFactory.Piece;

namespace SpaceshipFactory.Command
{
    public class RemovePiecesCommand : IModificationCommand
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
            var spaceship = GetSpaceshipByName(_spaceshipName);
            if (spaceship == null)
            {
                Logger.PrintError($"Spaceship '{_spaceshipName}' not found.");
                return;
            }

            foreach (var modification in _modifications)
            {
                string pieceName = modification.Key;
                int quantity = modification.Value;

                for (var i = 0; i < quantity; i++)
                {
                    var piece = spaceship.GetPieceByName(pieceName);
                    if (piece == null)
                    {
                        Logger.PrintError($"Piece '{pieceName}' not found in spaceship '{_spaceshipName}'.");
                        continue;
                    }

                    if (!spaceship.RemovePiece(piece))
                    {
                        Logger.PrintError($"Failed to remove piece '{pieceName}' from spaceship '{_spaceshipName}'.");
                    }
                }
            }

            Logger.PrintResult($"Successfully removed specified pieces from spaceship '{_spaceshipName}'.");
        }

        public bool Verify(IReadOnlyList<string> args)
        {
            if (args.Count < 3)
            {
                Logger.PrintError("REMOVE command expects at least a spaceship name and one piece to remove.");
                return false;
            }

            if (args[1] != "WITHOUT")
            {
                Logger.PrintError("Expected 'WITHOUT' keyword in REMOVE command.");
                return false;
            }

            for (int i = 2; i < args.Count; i += 2)
            {
                if (int.TryParse(args[i], out int quantity) && quantity >= 1) continue;
                Logger.PrintError($"'{args[i]}' is not a valid quantity.");
                return false;
            }

            return true;
        }

        private Spaceship? GetSpaceshipByName(string name)
        {
            return ProductionManager.GetSpaceship(name);
        }
    }
}
