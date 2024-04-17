using System;
using System.Collections.Generic;

namespace SpaceshipFactory;

public abstract class ProductionManager
{

    private static readonly Dictionary<string, Dictionary<string, int>> ShipComponents =
        new Dictionary<string, Dictionary<string, int>>
        {
            {
                "Explorer", new Dictionary<string, int>
                {
                    { "Hull_HE1", 1 },
                    { "Engine_EE1", 1 },
                    { "Wings_WE1", 1 },
                    { "Thruster_TE1", 1 }
                }
            },
            {
                "Speeder", new Dictionary<string, int>
                {
                    { "Hull_HS1", 1 },
                    { "Engine_ES1", 1 },
                    { "Wings_WS1", 1 },
                    { "Thruster_TS1", 2 }
                }
            },
            {
                "Cargo", new Dictionary<string, int>
                {
                    { "Hull_HC1", 1 },
                    { "Engine_EC1", 1 },
                    { "Wings_WC1", 1 },
                    { "Thruster_TC1", 1 },
                }
            }
        };

    private static readonly Dictionary<string, Dictionary<string, int>> Components = 
        new Dictionary<string, Dictionary<string, int>>
        {
        {
            "Coque", new Dictionary<string, int>
            {
                { "Hull_HE1", 1 },
                { "Hull_HS1", 1 },
                { "Hull_HC1", 1 }
            }
        },
        {
            "Moteur", new Dictionary<string, int>
            {
                { "Engine_EE1", 1 },
                { "Engine_ES1", 1 },
                { "Engine_EC1", 1 }
            }
        },
        {
            "Aile", new Dictionary<string, int>
            {
                { "Wings_WE1", 1 },
                { "Wings_WS1", 1 },
                { "Wings_WC1", 1 }
            }
        },
        {
            "Propulseur", new Dictionary<string, int>
            {
                { "Thruster_TE1", 1 },
                { "Thruster_TS1", 1 },
                { "Thruster_TC1", 1 }
            }
        }
    };
    
    public static void Produce(string[] args)
    {
        if (args.Length % 2 != 0)
        {
            // TODO: Verify
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

            if (!ShipComponents.ContainsKey(spaceship))
            {
                Logger.PrintError($"Unknown spaceship type: {spaceship}");
                return;
            }

            bool isStockAvailable = ShipComponents[spaceship]
                .All(part => InventoryManager.CheckStock(part.Key, part.Value * quantity));

            if (isStockAvailable)
            {
                Logger.PrintInstruction("PRODUCING", $"{quantity} {spaceship}(s)");
                foreach (var part in ShipComponents[spaceship])
                {
                    Logger.PrintInstruction("GET_OUT_STOCK", $"{part.Value * quantity} {part.Key}");
                }

                Assemble(spaceship, quantity);


                Logger.PrintResult($"FINISHED {quantity} {spaceship}(s)");
            }
            else
            {
                Logger.PrintError("Insufficient stock to complete production.");
                foreach (var part in ShipComponents[spaceship])
                {
                    InventoryManager.RestoreStock(part.Key, part.Value * quantity);
                }
            }
        }
    }


    private static void Assemble(string spaceship, int quantity)
    {
        var assemblySteps = new Dictionary<string, List<string>>();
        var componentsNeeded = ShipComponents[spaceship]
            .GroupBy(pair => pair.Key)
            .ToDictionary(group => group.Key, group => group.Sum(pair => pair.Value * quantity));

        string currentAssembly = "TMP1";
        List<string> componentsToAssemble = new List<string>();

        foreach (var component in componentsNeeded)
        {
            for (var i = 0; i < component.Value; i++)
            {
                componentsToAssemble.Add(component.Key);
            }

            if (componentsToAssemble.Count <= 1) continue;
            Logger.PrintInstruction("ASSEMBLE", $"{currentAssembly} {string.Join(" ", componentsToAssemble)}");
            componentsToAssemble.Clear();
            componentsToAssemble.Add(currentAssembly);
            currentAssembly = $"TMP{int.Parse(currentAssembly[3..]) + 1}";
        }
        
        if (componentsToAssemble.Count > 1)
        {
            Logger.PrintInstruction("ASSEMBLE", $"{currentAssembly} {string.Join(" ", componentsToAssemble)}");
        }

        Logger.PrintInstruction("FINISHED", spaceship);
    }
    
}