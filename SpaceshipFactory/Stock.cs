using SpaceshipFactory.Piece;

namespace SpaceshipFactory;

public class Stock
{
    private readonly Dictionary<Spaceship, uint> _spaceships = new();
    private readonly Dictionary<Engine, uint> _engines = new();
    private readonly Dictionary<Hull, uint> _hulls = new();
    private readonly Dictionary<Thruster, uint> _thrusters = new();
    private readonly Dictionary<Wings, uint> _wings = new();

    public void Add(Spaceship spaceship, uint quantity)
    {
        if (_spaceships.ContainsKey(spaceship))
        {
            _spaceships[spaceship] += quantity;
        }
        _spaceships.Add(spaceship, quantity);
    }

    public void Add(Engine engine, uint quantity)
    {
        if (_engines.ContainsKey(engine))
        {
            _engines[engine] += quantity;
        }
        _engines.Add(engine, quantity);
    }

    public void Add(Hull hull, uint quantity)
    {
        if (_hulls.ContainsKey(hull))
        {
            _hulls[hull] += quantity;
        }
        _hulls.Add(hull, quantity);
    }

    public void Add(Thruster thruster, uint quantity)
    {
        if (_thrusters.ContainsKey(thruster))
        {
            _thrusters[thruster] += quantity;
        }
        _thrusters.Add(thruster, quantity);
    }

    public void Add(Wings wings, uint quantity)
    {
        if (_wings.ContainsKey(wings))
        {
            _wings[wings] += quantity;
        }
        _wings.Add(wings, quantity);
    }

    public override string ToString()
    {
        string str = "";
        foreach (var kv in _spaceships)
        {
            str += $"{kv.Value} {kv.Key}\n";
        }
        foreach (var kv in _engines)
        {
            str += $"{kv.Value} {kv.Key}\n";
        }
        foreach (var kv in _hulls)
        {
            str += $"{kv.Value} {kv.Key}\n";
        }
        foreach (var kv in _thrusters)
        {
            str += $"{kv.Value} {kv.Value}\n";
        }
        foreach (var kv in _wings)
        {
            str += $"{kv.Value} {kv.Key}\n";
        }
        return str;
    }
}
