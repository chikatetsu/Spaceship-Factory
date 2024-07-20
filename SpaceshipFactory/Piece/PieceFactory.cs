namespace SpaceshipFactory.Piece;

public static class PieceFactory
{
    public static Piece? CreatePiece(string name)
    {
        return name switch
        {
            "Hull_HE1" => new Hull("Hull_HE1"),
            "Hull_HS1" => new Hull("Hull_HS1"),
            "Hull_HC1" => new Hull("Hull_HC1"),
            "Engine_EE1" => new Engine("Engine_EE1"),
            "Engine_ES1" => new Engine("Engine_ES1"),
            "Engine_EC1" => new Engine("Engine_EC1"),
            "Wings_WE1" => new Wings("Wings_WE1"),
            "Wings_WS1" => new Wings("Wings_WS1"),
            "Wings_WC1" => new Wings("Wings_WC1"),
            "Thruster_TE1" => new Thruster("Thruster_TE1"),
            "Thruster_TS1" => new Thruster("Thruster_TS1"),
            "Thruster_TC1" => new Thruster("Thruster_TC1"),
            _ => null
        };
    }

    public static Piece? CreatePiece(string name, Boolean isAssembly)
    {
        if (!isAssembly) return null;
        return new Assembly(name);
    }
}

