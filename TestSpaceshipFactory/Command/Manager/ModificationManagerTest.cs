using SpaceshipFactory.Command;
using SpaceshipFactory.Command.Manager;

namespace TestSpaceshipFactory.Command
{
    public class ModificationManagerTest
    {
        public ModificationManagerTest()
        {
            var addTemplateCommand = new ModificationManager();
            string[] addTemplateArgs = { "ADD_TEMPLATE", "Speed", "Hull_HS1", "Engine_ES1", "Wings_WS1", "Wings_WS1", "Thruster_TS1", "Thruster_TS1" };
            addTemplateCommand.Verify(addTemplateArgs);
            addTemplateCommand.Execute();

            var produceCommand = new ModificationManager();
            string[] produceArgs = { "PRODUCE", "1", "Speed" };
            produceCommand.Verify(produceArgs);
            produceCommand.Execute();
        }
        
        [Fact]
        public void Verify_AddPiecesCommand_ReturnsTrue()
        {
            var command = new ModificationManager();
            string[] args = { "Speed", "WITH", "1", "Engine_EE1", "2", "Wings_WE1" };
            Assert.True(command.Verify(args));
        }

        [Fact]
        public void Verify_RemovePiecesCommand_ReturnsTrue()
        {
            var command = new ModificationManager();
            string[] args = { "Speed", "WITHOUT", "1", "Engine_ES1", "2", "Wings_WS1" };
            Assert.True(command.Verify(args));
        }

        [Fact]
        public void Verify_ReplacePiecesCommand_ReturnsTrue()
        {
            var command = new ModificationManager();
            string[] args = { "Speed", "REPLACE", "1", "Engine_ES1", "2", "Wings_WS1", "WITH", "2", "Engine_EE1", "2", "Wings_WE1" };
            Assert.True(command.Verify(args));
        }

        [Fact]
        public void Verify_InvalidCommand_ReturnsFalse()
        {
            var command = new ModificationManager();
            string[] args = { "Speed", "INVALID", "1", "Engine_ES1" };
            Assert.False(command.Verify(args));
        }

        [Fact]
        public void Execute_AddPiecesCommand_AddsPieces()
        {
            var command = new ModificationManager();
            string[] args = { "Speed", "WITH", "1", "Engine_EE1", "2", "Wings_WE1" };
            command.Verify(args);
            command.Execute();

            var spaceship = ProductionManager.GetSpaceship("Speed");
            Assert.NotNull(spaceship);
            if (spaceship != null)
            {
                Assert.Equal(1, spaceship.Engines.Count(engine => engine?.Name == "Engine_EE1"));
                Assert.Equal(2, spaceship.Wings.Count(wings => wings?.Name == "Wings_WE1"));
            }
        }

        [Fact]
        public void Execute_RemovePiecesCommand_RemovesPieces()
        {
            var command = new ModificationManager();
            string[] args = { "Speed", "WITHOUT", "1", "Engine_ES1", "2", "Wings_WS1" };
            command.Verify(args);
            command.Execute();

            var spaceship = ProductionManager.GetSpaceship("Speed");
            Assert.NotNull(spaceship);
            Assert.Equal(0, spaceship.Engines.Count(engine => engine?.Name == "Engine_ES1"));
            Assert.Equal(0, spaceship.Wings.Count(wings => wings?.Name == "Wings_WS1"));
        }

        [Fact]
        public void Execute_ReplacePiecesCommand_ReplacesPieces()
        {
            var command = new ModificationManager();
            string[] args = { "Speed", "REPLACE", "1", "Engine_ES1", "2", "Wings_WS1", "WITH", "2", "Engine_EE1", "2", "Wings_WE1" };
            command.Verify(args);
            command.Execute();

            var spaceship = ProductionManager.GetSpaceship("Speed");
            Assert.NotNull(spaceship);
            Assert.Equal(0, spaceship.Engines.Count(engine => engine?.Name == "Engine_ES1"));
            Assert.Equal(2, spaceship.Engines.Count(engine => engine?.Name == "Engine_EE1"));
            Assert.Equal(0, spaceship.Wings.Count(wings => wings?.Name == "Wings_WS1"));
            Assert.Equal(2, spaceship.Wings.Count(wings => wings?.Name == "Wings_WE1"));
        }
    }
}
