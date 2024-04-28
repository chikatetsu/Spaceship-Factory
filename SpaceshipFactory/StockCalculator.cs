using SpaceshipFactory.Piece;
using System.Collections.Generic;
using System.Linq;

namespace SpaceshipFactory
{
    public class StockCalculator
    {
        private static Dictionary<string, Spaceship> _spaceshipModels = new();

        static StockCalculator()
        {
            _spaceshipModels = new Dictionary<string, Spaceship>
            {
                ["Explorer"] = new Spaceship("Explorer")
                    .AddPiece(new Hull("Hull_HE1"), 1)
                    .AddPiece(new Engine("Engine_EE1"), 1)
                    .AddPiece(new Wings("Wings_WE1"), 1)
                    .AddPiece(new Thruster("Thruster_TE1"), 1),
                ["Speeder"] = new Spaceship("Speeder")
                    .AddPiece(new Hull("Hull_HS1"), 1)
                    .AddPiece(new Engine("Engine_ES1"), 1)
                    .AddPiece(new Wings("Wings_WS1"), 1)
                    .AddPiece(new Thruster("Thruster_TS1"), 2),
                ["Cargo"] = new Spaceship("Cargo")
                    .AddPiece(new Hull("Hull_HC1"), 1)
                    .AddPiece(new Engine("Engine_EC1"), 1)
                    .AddPiece(new Wings("Wings_WC1"), 1)
                    .AddPiece(new Thruster("Thruster_TC1"), 1)
            };
        }

        public static Dictionary<string, int> CalculateNeededStocks(string[] spaceshipNames)
        {
            var totalNeededParts = new Dictionary<string, int>();

            foreach (var name in spaceshipNames)
            {
                if (_spaceshipModels.TryGetValue(name, out var spaceshipModel))
                {
                    foreach (var piece in spaceshipModel.Pieces)
                    {
                        int pieceCount = (int)piece.Value; 

                        if (totalNeededParts.ContainsKey(piece.Key.Name))
                        {
                            totalNeededParts[piece.Key.Name] += pieceCount;
                        }
                        else
                        {
                            totalNeededParts[piece.Key.Name] = pieceCount;
                        }
                    }
                }
                else
                {
                    Logger.PrintError($"Unknown spaceship model: {name}");
                }
            }

            return totalNeededParts;
        }

        public static void PrintNeededStocks(string[] args)
        {
            var spaceshipQuantities = ParseInput(args);
            foreach (var entry in spaceshipQuantities)
            {
                string spaceshipName = entry.Key;
                int quantity = entry.Value;
                Logger.PrintResult($"{spaceshipName} {quantity} :");
                
                var spaceship = _spaceshipModels[spaceshipName];
                foreach (var piece in spaceship.Pieces)
                {
                    for (int i = 0; i < piece.Value * quantity; i++)
                    {
                        Logger.PrintResult($"1 {piece.Key.Name}");
                    }
                }
            }

            Logger.PrintResult("Total:");
            PrintTotalStocks(spaceshipQuantities);
        }
        
        private static Dictionary<string, int> ParseInput(string[] args)
        {
            var spaceshipQuantities = new Dictionary<string, int>();
            for (int i = 0; i < args.Length; i += 2)
            {
                if (!int.TryParse(args[i], out int quantity) || quantity < 1)
                {
                    Logger.PrintError($"Invalid quantity: {args[i]}");
                    continue;
                }

                string modelName = args[i + 1];
                if (!_spaceshipModels.ContainsKey(modelName))
                {
                    Logger.PrintError($"Unknown spaceship model: {modelName}");
                    continue;
                }

                if (spaceshipQuantities.ContainsKey(modelName))
                {
                    spaceshipQuantities[modelName] += quantity;
                }
                else
                {
                    spaceshipQuantities[modelName] = quantity;
                }
            }
            return spaceshipQuantities;
        }
        private static void PrintTotalStocks(Dictionary<string, int> spaceshipQuantities)
        {
            var totalPartsNeeded = new Dictionary<string, int>();

            foreach (var kvp in spaceshipQuantities)
            {
                var spaceship = _spaceshipModels[kvp.Key];
                foreach (var piece in spaceship.Pieces)
                {
                    var pieceCount = (int)piece.Value;
                    if (totalPartsNeeded.ContainsKey(piece.Key.Name))
                    {
                        totalPartsNeeded[piece.Key.Name] += pieceCount * kvp.Value;
                    }
                    else
                    {
                        totalPartsNeeded[piece.Key.Name] = pieceCount * kvp.Value;
                    }
                }
            }

            foreach (var part in totalPartsNeeded)
            {
                Logger.PrintResult($"{part.Value} {part.Key}");
            }
        }
    }
}
