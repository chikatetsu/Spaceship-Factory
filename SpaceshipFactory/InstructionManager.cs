using SpaceshipFactory.Piece;
using System.Collections.Generic;

namespace SpaceshipFactory
{
    public class InstructionManager
    {
        public static readonly List<ISpaceshipFactory> ShipFactories = new()
        {
            new ExplorerFactory(),
            new SpeederFactory(),
            new CargoFactory()
        };

        public static void PrintInstructions(Spaceship spaceship, uint quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                Logger.PrintInstruction("PRODUCING", $"{spaceship.Name}");

                if (spaceship.Hull != null)
                {
                    Logger.PrintInstruction("GET_OUT_OF_STOCK", $"1 {spaceship.Hull.Name}");
                }

                foreach (var engine in spaceship.Engines)
                {
                    Logger.PrintInstruction("GET_OUT_OF_STOCK", $"1 {engine.Name}");
                }

                foreach (var wings in spaceship.Wings)
                {
                    Logger.PrintInstruction("GET_OUT_OF_STOCK", $"1 {wings.Name}");
                }

                foreach (var thruster in spaceship.Thrusters)
                {
                    Logger.PrintInstruction("GET_OUT_OF_STOCK", $"1 {thruster.Name}");
                }

                bool firstLoop = true;
                string currentAssembly = "";

                if (spaceship.Hull != null)
                {
                    currentAssembly = spaceship.Hull.Name;
                }

                foreach (var engine in spaceship.Engines)
                {
                    if (firstLoop && string.IsNullOrEmpty(currentAssembly))
                    {
                        currentAssembly = engine.Name;
                    }
                    else if (firstLoop)
                    {
                        Logger.PrintInstruction("ASSEMBLE", $"{currentAssembly} + {engine.Name}");
                        currentAssembly += $", {engine.Name}";
                        firstLoop = false;
                    }
                    else
                    {
                        Logger.PrintInstruction("ASSEMBLE", $"[{currentAssembly}] + {engine.Name}");
                        currentAssembly += $", {engine.Name}";
                    }
                }

                foreach (var wings in spaceship.Wings)
                {
                    if (firstLoop && string.IsNullOrEmpty(currentAssembly))
                    {
                        currentAssembly = wings.Name;
                    }
                    else if (firstLoop)
                    {
                        Logger.PrintInstruction("ASSEMBLE", $"{currentAssembly} + {wings.Name}");
                        currentAssembly += $", {wings.Name}";
                        firstLoop = false;
                    }
                    else
                    {
                        Logger.PrintInstruction("ASSEMBLE", $"[{currentAssembly}] + {wings.Name}");
                        currentAssembly += $", {wings.Name}";
                    }
                }

                foreach (var thruster in spaceship.Thrusters)
                {
                    if (firstLoop && string.IsNullOrEmpty(currentAssembly))
                    {
                        currentAssembly = thruster.Name;
                    }
                    else if (firstLoop)
                    {
                        Logger.PrintInstruction("ASSEMBLE", $"{currentAssembly} + {thruster.Name}");
                        currentAssembly += $", {thruster.Name}";
                        firstLoop = false;
                    }
                    else
                    {
                        Logger.PrintInstruction("ASSEMBLE", $"[{currentAssembly}] + {thruster.Name}");
                        currentAssembly += $", {thruster.Name}";
                    }
                }

                Logger.PrintInstruction("FINISHED", spaceship.Name);
            }
        }
    }
}
