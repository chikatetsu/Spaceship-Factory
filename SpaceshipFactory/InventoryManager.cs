namespace SpaceshipFactory;

public static class InventoryManager
{
    private static Dictionary<string, int> Stock = new Dictionary<string, int>
    {
        { "Hull_HE1", 10 },
        { "Engine_EE1", 10 },
        { "Wings_WE1", 10 },
        { "Thruster_TE1", 20 },
        { "Hull_HS1", 10 },
        { "Engine_ES1", 10 },
        { "Wings_WS1", 10 },
        { "Thruster_TS1", 20 },
        { "Hull_HC1", 10 },
        { "Engine_EC1", 10 },
        { "Wings_WC1", 10 },
        { "Thruster_TC1", 20 }
    };

    public static bool CheckStock(string part, int quantity)
    {
        if (!Stock.ContainsKey(part) || Stock[part] < quantity) return false;
        Stock[part] -= quantity;
        return true;
    }

    public static void RestoreStock(string part, int quantity)
    {
        if (Stock.ContainsKey(part))
        {
            Stock[part] += quantity;
        }
    }

    public static void PrintStock()
    {
        foreach (var item in Stock)
        {
            Console.WriteLine($"{item.Key}: {item.Value}");
        }
    }
}