using SpaceshipFactory.Piece;

namespace SpaceshipFactory
{
    public static class ProductionManager
    {
        public static readonly Dictionary<string, Spaceship> ShipModels = new()
        {
            {"Explorer", new Spaceship("Explorer", new Hull("Hull_HS1"), new Engine("Engine_EE1"), new Wings("Wings_WE1"), new Thruster("Thruster_TE1"))},
            {"Speeder", new Spaceship("Speeder", new Hull("Hull_HS1"), new Engine("Engine_ES1"), new Wings("Wings_WS1"), new Thruster("Thruster_TS1"))},
            {"Cargo", new Spaceship("Cargo", new Hull("Hull_HC1"), new Engine("Engine_EC1"), new Wings("Wings_WC1"), new Thruster("Thruster_TC1"))}
        };

        public static void Produce(string[] args)
        {
            if (args.Length % 2 != 0)
            {
                Logger.PrintError("Arguments must be in pairs of quantity and spaceship model.");
                return;
            }
            
            for (int i = 0; i < args.Length; i += 2)
            {
                string quantityArg = args[i];
                string modelArg = args[i + 1];

                if (!int.TryParse(quantityArg, out int quantity) || quantity < 1)
                {
                    Logger.PrintError($"Invalid quantity '{quantityArg}'. Expected a positive integer.");
                    continue;
                }

                if (!ShipModels.ContainsKey(modelArg))
                {
                    Logger.PrintError($"Spaceship model '{modelArg}' is not available.");
                    continue;
                }

                Assemble(modelArg, quantity);
            }
        }


        // TODO: Implement the rest of the commands (3.4 Édition des instructions d’assemblage)
        private static void Assemble(string spaceship, int quantity)
        {
            if (Stock.Verify(spaceship, quantity))
            {
                for (int i = 0; i < quantity; i++)
                {
                    Logger.PrintInstruction("PRODUCING", $"1 {ShipModels[spaceship]}");
                    Logger.PrintInstruction("GET_OUT_STOCK", $"1 {ShipModels[spaceship]}");
                    Logger.PrintResult($"FINISHED {spaceship}");
                }
            }
            else
            {
                Logger.PrintError("Unable to start production due to insufficient stock.");
            }
        }
    }
}