namespace SpaceshipFactory.Command;

public interface IModificationCommand : ICommand
{
    void SetArgs(string spaceshipName, Dictionary<string, int> modifications);
}