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
                    quantity = newQuantity;
                    continue;
                }

                Spaceship? spaceship = ShipModels.Find(spaceship => spaceship.Name == modelArg);

                if (spaceship == null)
                {
                    Logger.PrintError($"Spaceship model '{modelArg}' is not available.");
                    continue;
                }

                Instruction[] instructions = new Instruction[]
                {
                    new Instruction("GET_OUT_OF_STOCK", "1 Hull_HE1"),
                    new Instruction("GET_OUT_OF_STOCK", "1 Engine_EE1"),
                    new Instruction("GET_OUT_OF_STOCK", "1 Wings_WE1"),
                    new Instruction("GET_OUT_OF_STOCK", "1 Thruster_TE1"),
                    new Instruction("ASSEMBLE", "TMP1 Hull_HS1 Engine_HS1"),
                    new Instruction("ASSEMBLE", "TMP1 Wings_HS1"),
                    new Instruction("ASSEMBLE", "TMP3[TMP1, Wings_HS1] Thruster_HS1"),
                    new Instruction("ASSEMBLE", "TMP3 Thruster_HS1")
                };

                for(int j = 0; j <= quantity; j++)
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