using SpaceshipFactory.Piece;

namespace SpaceshipFactory;

public static class Stock
{
    private static readonly Dictionary<Spaceship, uint> Spaceships = new();
    private static readonly Dictionary<Engine, uint> Engines = new()
    {
        { new Engine("Engine_EE1"), 5 },
        { new Engine("Engine_ES1"), 9 },
        { new Engine("Engine_EC1"), 11 },
    };
    private static readonly Dictionary<Hull, uint> Hulls = new()
    {
        { new Hull("Hull_HE1"), 10 },
        { new Hull("Hull_HS1"), 15 },
        { new Hull("Hull_HC1"), 0 },
    };
    private static readonly Dictionary<Thruster, uint> Thrusters = new()
    {
        { new Thruster("Thruster_TE1"), 20 },
        { new Thruster("Thruster_TS1"), 5 },
        { new Thruster("Thruster_TC1"), 18 },
    };
    private static readonly Dictionary<Wings, uint> Wings = new()
    {
        { new Wings("Wings_WE1"), 32 },
        { new Wings("Wings_WS1"), 16 },
        { new Wings("Wings_WC1"), 24 },
    };

    public static void Add(Spaceship spaceship, uint quantity)
    {
        if (Spaceships.ContainsKey(spaceship))
        {
            Spaceships[spaceship] += quantity;
        }
        Spaceships.Add(spaceship, quantity);
    }

    public static void Add(Engine engine, uint quantity)
    {
        if (Engines.ContainsKey(engine))
        {
            Engines[engine] += quantity;
        }
        Engines.Add(engine, quantity);
    }

    public static void Add(Hull hull, uint quantity)
    {
        if (Hulls.ContainsKey(hull))
        {
            Hulls[hull] += quantity;
        }
        Hulls.Add(hull, quantity);
    }

    public static void Add(Thruster thruster, uint quantity)
    {
        if (Thrusters.ContainsKey(thruster))
        {
            Thrusters[thruster] += quantity;
        }
        Thrusters.Add(thruster, quantity);
    }

    public static void Add(Wings wings, uint quantity)
    {
        if (Wings.ContainsKey(wings))
        {
            Wings[wings] += quantity;
        }
        Wings.Add(wings, quantity);
    }

    public static string GetStocks()
    {
        string str = "";
        foreach (var kv in Spaceships)
        {
            if (kv.Value == 0)
            {
                continue;
            }
            str += $"{kv.Value} {kv.Key}\n";
        }
        foreach (var kv in Engines)
        {
            if (kv.Value == 0)
            {
                continue;
            }
            str += $"{kv.Value} {kv.Key}\n";
        }
        foreach (var kv in Hulls)
        {
            if (kv.Value == 0)
            {
                continue;
            }
            str += $"{kv.Value} {kv.Key}\n";
        }
        foreach (var kv in Thrusters)
        {
            if (kv.Value == 0)
            {
                continue;
            }
            str += $"{kv.Value} {kv.Key}\n";
        }
        foreach (var kv in Wings)
        {
            if (kv.Value == 0)
            {
                continue;
            }
            str += $"{kv.Value} {kv.Key}\n";
        }
        return str;
    }
}
