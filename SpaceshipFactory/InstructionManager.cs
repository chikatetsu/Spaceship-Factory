using SpaceshipFactory.Piece;

namespace SpaceshipFactory
{
    internal class InstructionManager
    {
        private static readonly List<Spaceship?> ShipModels = new()
        {
            new Spaceship("Explorer"),
            new Spaceship("Speeder"),
            new Spaceship("Cargo")
        };
        public static void Init(string[] args)
        {
            for (int i = 0; i < args.Length; i += 2)
            {
                string quantityArg = args[i];
                string modelArg = args[i + 1];
                int quantity = 1;


                if (!int.TryParse(quantityArg, out int newQuantity) || newQuantity < 1)
                {
                    continue;
                }

                quantity = int.Parse(quantityArg);

                Spaceship? spaceship = ShipModels.Find(spaceship => spaceship.Name == modelArg);

                if (spaceship == null)
                {
                    Logger.PrintError($"Spaceship model '{modelArg}' is not available.");
                    continue;
                }

                Instruction[] instructions = new Instruction[0];


                switch (spaceship.Name)
                {
                    case "Explorer":
                        instructions = new Instruction[]
                        {
                            new Instruction("GET_OUT_OF_STOCK", "1 Hull_HE1"),
                            new Instruction("GET_OUT_OF_STOCK", "1 Engine_EE1"),
                            new Instruction("GET_OUT_OF_STOCK", "1 Wings_WE1"),
                            new Instruction("GET_OUT_OF_STOCK", "1 Thruster_TE1"),
                            new Instruction("ASSEMBLE", "TMP1 Hull_HE1 Engine_EE1"),
                            new Instruction("ASSEMBLE", "TMP1 Wings_WE1"),
                            new Instruction("ASSEMBLE", "TMP3[TMP1, Wings_WE1] Thruster_TE1"),
                        };
                        break;
                    case "Speeder":
                        instructions = new Instruction[]
                        {
                            new Instruction("GET_OUT_OF_STOCK", "1 Hull_HS1"),
                            new Instruction("GET_OUT_OF_STOCK", "1 Engine_ES1"),
                            new Instruction("GET_OUT_OF_STOCK", "1 Wings_WS1"),
                            new Instruction("GET_OUT_OF_STOCK", "2 Thruster_TS1"),
                            new Instruction("ASSEMBLE", "TMP1 Hull_HS1 Engine_ES1"),
                            new Instruction("ASSEMBLE", "TMP1 Wings_WS1"),
                            new Instruction("ASSEMBLE", "TMP3[TMP1, Wings_WS1] Thruster_TS1"),
                            new Instruction("ASSEMBLE", "TMP3 Thruster_TS1")
                        };
                        break;
                    case "Cargo":
                        instructions = new Instruction[]
                        {
                            new Instruction("GET_OUT_OF_STOCK", "1 Hull_HC1"),
                            new Instruction("GET_OUT_OF_STOCK", "1 Engine_EC1"),
                            new Instruction("GET_OUT_OF_STOCK", "1 Wings_WC1"),
                            new Instruction("GET_OUT_OF_STOCK", "1 Thruster_TC1"),
                            new Instruction("ASSEMBLE", "TMP1 Hull_HC1 Engine_EC1"),
                            new Instruction("ASSEMBLE", "TMP1 Wings_WC1"),
                            new Instruction("ASSEMBLE", "TMP3[TMP1, Wings_WC1] Thruster_TC1"),
                        };
                        break;
                }

                for (int j = 0; j < quantity; j++)
                {
                    Logger.PrintInstruction("\nPRODUCING", $"{spaceship.Name} {j+1}");
                    foreach (Instruction instruction in instructions)
                    {
                        Logger.PrintInstruction(instruction.Header, instruction.Value);
                    }
                    Logger.PrintInstruction("FINISHED", $"{spaceship.Name} {j+1}");
                }

            }

        }
    }
}