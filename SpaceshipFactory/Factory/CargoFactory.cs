﻿using SpaceshipFactory;
using SpaceshipFactory.Piece;

public class CargoFactory : ISpaceshipFactory
{
    public Spaceship CreateSpaceship()
    {
        return new Spaceship("Cargo", new Dictionary<Piece, uint>
        {
            { new Hull("Hull_HC1"), 1 },
            { new Engine("Engine_EC1"), 1 },
            { new Wings("Wings_WC1"), 1 },
            { new Thruster("Thruster_TC1"), 1 }
        });
    }
}
