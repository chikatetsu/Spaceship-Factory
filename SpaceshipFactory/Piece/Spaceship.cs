namespace SpaceshipFactory.Piece
{
    public class Spaceship
    {
        public readonly string Name;
        public Hull? Hull { get; private set; }
        public List<Engine?> Engines { get; }
        public List<Wings?> Wings { get; }
        public List<Thruster?> Thrusters { get; }

        public Spaceship(string name)
        {
            Name = name;
            Engines = new List<Engine?>();
            Wings = new List<Wings?>();
            Thrusters = new List<Thruster?>();
        }

        public bool AddPiece(Piece? piece)
        {
            if (piece == null) return false;

            switch (piece)
            {
                case Hull hull:
                    if (Hull != null)
                    {
                        Logger.PrintError("A spaceship can only have one hull.");
                        return false;
                    }
                    Hull = hull;
                    break;
                case Engine engine:
                    if (Engines.Count >= 2)
                    {
                        Logger.PrintError("A spaceship can only have up to two engines.");
                        return false;
                    }
                    Engines.Add(engine);
                    break;
                case Wings wings:
                    if (Wings.Count >= 2)
                    {
                        Logger.PrintError("A spaceship can only have up to two wings.");
                        return false;
                    }
                    Wings.Add(wings);
                    break;
                case Thruster thruster:
                    if (Thrusters.Count >= 3)
                    {
                        Logger.PrintError("A spaceship can only have up to three thrusters.");
                        return false;
                    }
                    Thrusters.Add(thruster);
                    break;
                default:
                    Logger.PrintError($"Unknown piece type: {piece.GetType().Name}");
                    return false;
            }
            return true;
        }

        public bool RemovePiece(Piece piece)
        {
            switch (piece)
            {
                case Hull hull when Hull == hull:
                    Hull = null;
                    break;
                case Engine engine when Engines.Contains(engine):
                    Engines.Remove(engine);
                    break;
                case Wings wings when Wings.Contains(wings):
                    Wings.Remove(wings);
                    break;
                case Thruster thruster when Thrusters.Contains(thruster):
                    Thrusters.Remove(thruster);
                    break;
                default:
                    return false;
            }
            return true;
        }

        public bool IsValid()
        {
            return Hull != null && Engines.Count >= 1 && Engines.Count <= 2 &&
                   Wings.Count >= 1 && Wings.Count <= 2 &&
                   Thrusters.Count >= 1 && Thrusters.Count <= 3 &&
                   (Thrusters.Count <= 2 || Engines.Count == 2);
        }

        public Piece? GetPieceByName(string pieceName)
        {
            if (Hull != null && Hull.Name == pieceName) return Hull;
            foreach (var engine in Engines.Where(engine => engine?.Name == pieceName))
            {
                return engine;
            }
            foreach (var wings in Wings.Where(wings => wings?.Name == pieceName))
            {
                return wings;
            }

            return Thrusters.FirstOrDefault(thruster => thruster?.Name == pieceName);
        }

        /** The following methods are generated by Rider's "Generate" feature. */
        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Spaceship spaceship)
            {
                return Name == spaceship.Name;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
