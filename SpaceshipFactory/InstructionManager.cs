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
            for (int i = 0; i < args.Length; i++)
            {
                /*
                ShipModels[0]?
                    .AddPiece(new Hull("Hull_HE1"), 1)
                    .AddPiece(new Engine("Engine_EE1"), 1)
                    .AddPiece(new Wings("Wings_WE1"), 1)
                    .AddPiece(new Thruster("Thruster_TE1"), 1);

                ShipModels[1]?
                    .AddPiece(new Hull("Hull_HS1"), 1)
                    .AddPiece(new Engine("Engine_ES1"), 1)
                    .AddPiece(new Wings("Wings_WS1"), 1)
                    .AddPiece(new Thruster("Thruster_TS1"), 2);

                ShipModels[2]?.AddPiece(new Hull("Hull_HC1"), 1)
                    .AddPiece(new Engine("Engine_EC1"), 1)
                    .AddPiece(new Wings("Wings_WC1"), 1)
                    .AddPiece(new Thruster("Thruster_TC1"), 1);

                */
                string modelArg = args[i];

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

                Logger.PrintInstruction("PRODUCING", $"{spaceship.Name}");
                foreach (Instruction instruction in instructions)
                {
                    Logger.PrintInstruction(instruction.Header, instruction.Value);
                }
                Logger.PrintInstruction("FINISHED", $"{spaceship.Name}");


            }

        }
    }
}