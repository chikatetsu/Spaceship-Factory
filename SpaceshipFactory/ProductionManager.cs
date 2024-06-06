using SpaceshipFactory.Piece;
using System.Collections.Generic;

namespace SpaceshipFactory
{
    public static class ProductionManager
    {
        private static Dictionary<string, Spaceship> _customTemplates = new();

        public static void Produce(Spaceship model, uint quantityToProduce)
        {
            var stock = Stock.Instance;
            if (!stock.IsStockSufficient(model, quantityToProduce))
            {
                Logger.PrintError("Unable to start production due to insufficient stock");
                return;
            }

            for (var i = 0; i < quantityToProduce; i++)
            {
                Spaceship newSpaceship = new(model.Name);

                foreach (var piece in model.Engines)
                {
                    if (!newSpaceship.AddPiece(piece))
                    {
                        Logger.PrintError($"Failed to add {piece.GetType().Name}");
                        return;
                    }
                }
                foreach (var piece in model.Wings)
                {
                    if (!newSpaceship.AddPiece(piece))
                    {
                        Logger.PrintError($"Failed to add {piece.GetType().Name}");
                        return;
                    }
                }
                foreach (var piece in model.Thrusters)
                {
                    if (!newSpaceship.AddPiece(piece))
                    {
                        Logger.PrintError($"Failed to add {piece.GetType().Name}");
                        return;
                    }
                }
                if (model.Hull != null)
                {
                    newSpaceship.AddPiece(model.Hull);
                }

                if (!newSpaceship.IsValid())
                {
                    Logger.PrintError($"Invalid spaceship configuration for {newSpaceship.Name}");
                    return;
                }

                stock.Add(newSpaceship, 1);
            }
            Logger.PrintResult("STOCK_UPDATED");
        }

        public static Spaceship? CreateSpaceship(string type)
        {
            if (_customTemplates.TryGetValue(type, out Spaceship? customTemplate))
            {
                Spaceship newSpaceship = new(customTemplate.Name);
                foreach (var piece in customTemplate.Engines) newSpaceship.AddPiece(piece);
                foreach (var piece in customTemplate.Wings) newSpaceship.AddPiece(piece);
                foreach (var piece in customTemplate.Thrusters) newSpaceship.AddPiece(piece);
                if (customTemplate.Hull != null) newSpaceship.AddPiece(customTemplate.Hull);
                return newSpaceship;
            }

            ISpaceshipFactory? factory = type switch
            {
                "Explorer" => new ExplorerFactory(),
                "Speeder" => new SpeederFactory(),
                "Cargo" => new CargoFactory(),
                _ => null
            };

            return factory?.CreateSpaceship();
        }

        public static void AddTemplate(string name, List<Piece.Piece> pieces)
        {
            Spaceship newTemplate = new(name);
            foreach (var piece in pieces)
            {
                if (!newTemplate.AddPiece(piece))
                {
                    Logger.PrintError($"Failed to add {piece.GetType().Name} to template {name}");
                    return;
                }
            }
            if (newTemplate.IsValid())
            {
                _customTemplates[name] = newTemplate;
                Logger.PrintResult($"Template {name} added successfully.");
            }
            else
            {
                Logger.PrintError($"Template {name} is invalid and cannot be added.");
            }
        }
    }
}
