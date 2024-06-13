namespace SpaceshipFactory.Command
{
    public class ModificationManager : ICommand
    {
        private readonly List<IModificationCommand> _modificationCommands = new();

        public void Execute()
        {
            foreach (var cmd in _modificationCommands)
            {
                cmd.Execute();
            }
        }

        public bool Verify(IReadOnlyList<string> args)
        {
            if (IsModificationCommand(args))
            {
                return true;
            }

            Logger.PrintError("Invalid modification command.");
            return false;
        }

        private bool IsModificationCommand(IReadOnlyList<string> args)
        {
            foreach (var arg in args)
            {
                if (arg.Contains("WITHOUT"))
                {
                    var parsedArgs = ParseModificationArgs(args);
                    var cmd = new RemovePiecesCommand();
                    cmd.SetArgs(parsedArgs.Item1, parsedArgs.Item2);
                    _modificationCommands.Add(cmd);
                    return true;
                }
                
                if (arg.Contains("WITH"))
                {
                    var parsedArgs = ParseModificationArgs(args);
                    var cmd = new AddPiecesCommand();
                    cmd.SetArgs(parsedArgs.Item1, parsedArgs.Item2);
                    _modificationCommands.Add(cmd);
                    return true;
                }

                if (arg.Contains("REPLACE"))
                {
                    var parsedArgs = ParseModificationArgsForReplace(args);
                    var cmd = new ReplacePiecesCommand();
                    cmd.SetArgs(parsedArgs.Item1, parsedArgs.Item2);
                    _modificationCommands.Add(cmd);
                    return true;
                }
            }

            return false;
        }


        private (string, Dictionary<string, int>) ParseModificationArgs(IReadOnlyList<string> args)
        {
            var spaceshipName = args[0];
            var modifications = new Dictionary<string, int>();

            for (var i = 2; i < args.Count; i += 2)
            {
                if (!int.TryParse(args[i], out int quantity)) continue;
                modifications[args[i + 1]] = quantity;
            }

            return (spaceshipName, modifications);
        }
        
        private (string, Dictionary<string, (int OldQuantity, string OldPiece, int NewQuantity, string NewPiece)>) ParseModificationArgsForReplace(IReadOnlyList<string> args)
        {
            string spaceshipName = args[0];
            var modifications = new Dictionary<string, (int OldQuantity, string OldPiece, int NewQuantity, string NewPiece)>();

            bool parsingOldPieces = true;
            List<(int Quantity, string Piece)> oldPieces = new();
            List<(int Quantity, string Piece)> newPieces = new();

            for (int i = 1; i < args.Count; i++)
            {
                if (args[i] == "REPLACE")
                {
                    continue;
                }

                if (args[i] == "WITH")
                {
                    parsingOldPieces = false;
                    continue;
                }

                if (parsingOldPieces)
                {
                    if (i + 1 >= args.Count || !int.TryParse(args[i], out int quantity))
                    {
                        Logger.PrintError("Invalid format for REPLACE command.");
                        break;
                    }

                    string piece = args[i + 1];
                    oldPieces.Add((quantity, piece));
                    i++;
                }
                else
                {
                    if (i + 1 >= args.Count || !int.TryParse(args[i], out int quantity))
                    {
                        Logger.PrintError("Invalid format for REPLACE command.");
                        break;
                    }

                    string piece = args[i + 1];
                    newPieces.Add((quantity, piece));
                    i++;
                }
            }

            if (oldPieces.Count != newPieces.Count)
            {
                Logger.PrintError("Mismatched number of old and new pieces in REPLACE command.");
                return (spaceshipName, modifications);
            }

            for (int j = 0; j < oldPieces.Count; j++)
            {
                modifications.Add($"{oldPieces[j].Piece}->{newPieces[j].Piece}",
                    (oldPieces[j].Quantity, oldPieces[j].Piece, newPieces[j].Quantity, newPieces[j].Piece));
            }

            return (spaceshipName, modifications);
        }
    }
}