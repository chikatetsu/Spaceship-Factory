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
                if (!arg.Contains("WITH") && !arg.Contains("WITHOUT") && !arg.Contains("REPLACE")) continue;
                var parsedArgs = ParseModificationArgs(args);
                if (arg.Contains("WITH"))
                {
                    var cmd = new AddPiecesCommand();
                    cmd.SetArgs(parsedArgs.Item1, parsedArgs.Item2);
                    _modificationCommands.Add(cmd);
                }
                else if (arg.Contains("WITHOUT"))
                {
                    var cmd = new RemovePiecesCommand();
                    cmd.SetArgs(parsedArgs.Item1, parsedArgs.Item2);
                    _modificationCommands.Add(cmd);
                }
                else if (arg.Contains("REPLACE"))
                {
                    var cmd = new ReplacePiecesCommand();
                    cmd.SetArgs(parsedArgs.Item1, parsedArgs.Item2);
                    _modificationCommands.Add(cmd);
                }

                return true;
            }

            return false;
        }

        private (string, Dictionary<string, int>) ParseModificationArgs(IReadOnlyList<string> args)
        {
            var spaceshipName = args[0];
            var modifications = new Dictionary<string, int>();

            for (var i = 1; i < args.Count; i++)
            {
                if (args[i] == "WITH" || args[i] == "WITHOUT" || args[i] == "REPLACE")
                {
                    continue;
                }

                if (!int.TryParse(args[i], out int quantity)) continue;
                modifications[args[i + 1]] = quantity;
                i++;
            }

            return (spaceshipName, modifications);
        }
    }
}