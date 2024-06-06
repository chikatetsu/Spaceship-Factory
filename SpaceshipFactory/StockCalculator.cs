using SpaceshipFactory.Piece;
using System.Collections.Generic;
using System.Linq;

namespace SpaceshipFactory
{
    public static class StockCalculator
    {
        public static Dictionary<string, uint> CalculateNeededStocks(string[] spaceshipNames)
        {
            var totalNeededParts = new Dictionary<string, uint>();

            foreach (var name in spaceshipNames)
            {
                var spaceship = InstructionManager.ShipFactories
                    .Select(factory => factory.CreateSpaceship())
                    .FirstOrDefault(spaceship => spaceship.Name == name);
            
                if (spaceship == null)
                {
                    Logger.PrintError($"Unknown spaceship model: {name}");
                    continue;
                }

                if (spaceship.Hull != null)
                {
                    AddPieceToTotalNeededParts(totalNeededParts, spaceship.Hull.Name, 1);
                }

                foreach (var engine in spaceship.Engines)
                {
                    AddPieceToTotalNeededParts(totalNeededParts, engine.Name, 1);
                }

                foreach (var wings in spaceship.Wings)
                {
                    AddPieceToTotalNeededParts(totalNeededParts, wings.Name, 1);
                }

                foreach (var thruster in spaceship.Thrusters)
                {
                    AddPieceToTotalNeededParts(totalNeededParts, thruster.Name, 1);
                }
            }

            return totalNeededParts;
        }

        private static void AddPieceToTotalNeededParts(Dictionary<string, uint> totalNeededParts, string pieceName, uint pieceCount)
        {
            if (!totalNeededParts.TryAdd(pieceName, pieceCount))
            {
                totalNeededParts[pieceName] += pieceCount;
            }
        }

        public static void PrintNeededStocks(Dictionary<Spaceship, uint>? spaceshipQuantities)
        {
            if (spaceshipQuantities == null)
            {
                return;
            }

            foreach ((Spaceship spaceship, uint quantity) in spaceshipQuantities)
            {
                Logger.PrintResult($"{spaceship.Name} {quantity} :");

                if (spaceship.Hull != null)
                {
                    PrintPieceStock(spaceship.Hull.Name, 1 * quantity);
                }

                foreach (var engine in spaceship.Engines)
                {
                    PrintPieceStock(engine.Name, 1 * quantity);
                }

                foreach (var wings in spaceship.Wings)
                {
                    PrintPieceStock(wings.Name, 1 * quantity);
                }

                foreach (var thruster in spaceship.Thrusters)
                {
                    PrintPieceStock(thruster.Name, 1 * quantity);
                }
            }

            Logger.PrintResult("Total:");
            PrintTotalStocks(spaceshipQuantities);
        }

        private static void PrintPieceStock(string pieceName, uint quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                Logger.PrintResult($"1 {pieceName}");
            }
        }

        private static void PrintTotalStocks(Dictionary<Spaceship, uint> spaceshipQuantities)
        {
            var totalPartsNeeded = new Dictionary<string, uint>();

            foreach ((Spaceship? spaceship, uint spaceshipQuantity) in spaceshipQuantities)
            {
                if (spaceship.Hull != null)
                {
                    AddPieceToTotalNeededParts(totalPartsNeeded, spaceship.Hull.Name, 1 * spaceshipQuantity);
                }

                foreach (var engine in spaceship.Engines)
                {
                    AddPieceToTotalNeededParts(totalPartsNeeded, engine.Name, 1 * spaceshipQuantity);
                }

                foreach (var wings in spaceship.Wings)
                {
                    AddPieceToTotalNeededParts(totalPartsNeeded, wings.Name, 1 * spaceshipQuantity);
                }

                foreach (var thruster in spaceship.Thrusters)
                {
                    AddPieceToTotalNeededParts(totalPartsNeeded, thruster.Name, 1 * spaceshipQuantity);
                }
            }

            foreach (var part in totalPartsNeeded)
            {
                Logger.PrintResult($"{part.Value} {part.Key}");
            }
        }
    }
}
