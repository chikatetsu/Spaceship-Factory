using SpaceshipFactory.Piece;

namespace SpaceshipFactory;

public class InstructionManager
{
    private static readonly List<Spaceship> ShipModels = new()
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

            if (!int.TryParse(quantityArg, out int newQuantity) || newQuantity < 1)
            {
                continue;
            }

            int quantity = int.Parse(quantityArg);

            Spaceship? spaceship = ShipModels.Find(spaceship => spaceship.Name == modelArg);

            if (spaceship == null)
            {
                Logger.PrintError($"Spaceship model '{modelArg}' is not available.");
                continue;
            }

            Instruction[] instructions = new Instruction[0];

            string hullPieceName;
            string enginePieceName;
            string wingsPieceName;
            string thrusterPieceName;

            switch (spaceship.Name)
            {
                case "Explorer":
                    hullPieceName = "Hull_HE1";
                    enginePieceName = "Engine_EE1";
                    wingsPieceName = "Wings_WE1";
                    thrusterPieceName = "Thruster_TE1";
                    instructions = new Instruction[]
                    {
                        new("GET_OUT_OF_STOCK", $"1 {hullPieceName}"),
                        new("GET_OUT_OF_STOCK", $"1 {enginePieceName}"),
                        new("GET_OUT_OF_STOCK", $"1 {wingsPieceName}"),
                        new("GET_OUT_OF_STOCK", $"1 {thrusterPieceName}"),
                        new("ASSEMBLE", $"TMP1 {hullPieceName} {enginePieceName}"),
                        new("ASSEMBLE", $"TMP1 {wingsPieceName}"),
                        new("ASSEMBLE", $"TMP3[TMP1, {wingsPieceName}] {thrusterPieceName}"),
                    };
                    break;
                case "Speeder":
                    hullPieceName = "Hull_HS1";
                    enginePieceName = "Engine_ES1";
                    wingsPieceName = "Wings_WS1";
                    thrusterPieceName = "Thruster_TS1";
                    instructions = new Instruction[]
                    {
                        new("GET_OUT_OF_STOCK", $"1 {hullPieceName}"),
                        new("GET_OUT_OF_STOCK", $"1 {enginePieceName}"),
                        new("GET_OUT_OF_STOCK", $"1 {wingsPieceName}"),
                        new("GET_OUT_OF_STOCK", $"2 {thrusterPieceName}"),
                        new("ASSEMBLE", $"TMP1 {hullPieceName} {enginePieceName}"),
                        new("ASSEMBLE", $"TMP1 {wingsPieceName}"),
                        new("ASSEMBLE", $"TMP3[TMP1, {wingsPieceName}] {thrusterPieceName}"),
                        new("ASSEMBLE", $"TMP3 {thrusterPieceName}")
                    };
                    break;
                case "Cargo":
                    hullPieceName = "Hull_HC1";
                    enginePieceName = "Engine_EC1";
                    wingsPieceName = "Wings_WC1";
                    thrusterPieceName = "Thruster_TC1";
                    instructions = new Instruction[]
                    {
                        new("GET_OUT_OF_STOCK", $"1 {hullPieceName}"),
                        new("GET_OUT_OF_STOCK", $"1 {enginePieceName}"),
                        new("GET_OUT_OF_STOCK", $"1 {wingsPieceName}"),
                        new("GET_OUT_OF_STOCK", $"1 {thrusterPieceName}"),
                        new("ASSEMBLE", $"TMP1 {hullPieceName} {enginePieceName}"),
                        new("ASSEMBLE", $"TMP1 {wingsPieceName}"),
                        new("ASSEMBLE", $"TMP3[TMP1, {wingsPieceName}] {thrusterPieceName}"),
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
