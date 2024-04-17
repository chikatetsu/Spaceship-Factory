namespace SpaceshipFactory;

public abstract class ProductionManager
{
    public static void Produce(string[] args)
    {
        if (args.Length % 2 != 0)
        {
            Logger.PrintError("Invalid arguments for PRODUCE");
            return;
        }

        for (var i = 0; i < args.Length; i += 2)
        {
            var spaceship = args[i];
            if (!int.TryParse(args[i + 1], out int quantity))
            {
                Logger.PrintError($"Invalid quantity for {spaceship}: {args[i + 1]}");
                return;
            }

            Logger.PrintInstruction("PRODUCING", $"{quantity} {spaceship}(s)");

            if (InventoryManager.CheckStock("Hull_HS1", quantity) &&
                InventoryManager.CheckStock("Engine_ES1", quantity) &&
                InventoryManager.CheckStock("Wings_WS1", quantity) &&
                InventoryManager.CheckStock("Thruster_TS1", quantity * 2))
            {
                Logger.PrintResult($"Production completed for {quantity} {spaceship}(s). Stock updated.");
            }
            else
            {
                Logger.PrintError("Insufficient stock to complete production.");
                InventoryManager.RestoreStock("Hull_HS1", quantity);
                InventoryManager.RestoreStock("Engine_ES1", quantity);
                InventoryManager.RestoreStock("Wings_WS1", quantity);
                InventoryManager.RestoreStock("Thruster_TS1", quantity * 2);
            }
        }
    }
}