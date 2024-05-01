using SpaceshipFactory.Piece;

namespace SpaceshipFactory;

public class InstructionManager
{
    public static readonly List<Spaceship> ShipModels = new()
    {
        new Spaceship("Explorer", new Dictionary<Piece.Piece, uint>
        {
            { new Hull("Hull_HE1"), 1 },
            { new Engine("Engine_EE1"), 1 },
            { new Wings("Wings_WE1"), 1 },
            { new Thruster("Thruster_TE1"), 1 }
        }),
        new Spaceship("Speeder", new Dictionary<Piece.Piece, uint>
        {
            { new Hull("Hull_HS1"), 1 },
            { new Engine("Engine_ES1"), 1 },
            { new Wings("Wings_WS1"), 1 },
            { new Thruster("Thruster_TS1"), 2 }
        }),
        new Spaceship("Cargo", new Dictionary<Piece.Piece, uint>
        {
            { new Hull("Hull_HC1"), 1 },
            { new Engine("Engine_EC1"), 1 },
            { new Wings("Wings_WC1"), 1 },
            { new Thruster("Thruster_TC1"), 1 }
        })
    };


    public static void PrintInstructions(Spaceship spaceship, uint quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            Logger.PrintInstruction("PRODUCING", $"{spaceship.Name}");

            foreach (var piece in spaceship.Pieces)
            {
                Logger.PrintInstruction("GET_OUT_OF_STOCK", $"{piece.Value} {piece.Key.Name}");
            }

            bool firstLoop = true;
            string currentAssembly = "";

            foreach (var piece in spaceship.Pieces)
            {
                for(int pieceQuantity = 0; pieceQuantity < piece.Value; pieceQuantity++)
                {
                    if (currentAssembly == "")
                    {
                        //Logger.PrintInstruction("ASSEMBLE", $"{piece.Key.Name}");
                        currentAssembly = $"{piece.Key.Name}";
                    }
                    else
                    {
                        if (firstLoop)
                        {
                            Logger.PrintInstruction("ASSEMBLE", $"{currentAssembly} {piece.Key.Name}");
                            currentAssembly = $"{currentAssembly}, {piece.Key.Name}";
                            firstLoop = false;
                        }
                        else
                        {
                            Logger.PrintInstruction("ASSEMBLE", $"[{currentAssembly}] {piece.Key.Name}");
                            currentAssembly = $"{currentAssembly}, {piece.Key.Name}";
                        }
                    }
                }
                
            }

            Logger.PrintInstruction("FINISHED", spaceship.Name);
        }
    }
}
