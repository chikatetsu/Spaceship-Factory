namespace SpaceshipFactory.Command;

public class AddPiecesCommand : IModificationCommand
{
    private string _spaceshipName;
    private Dictionary<string, int> _modifications;

    public void SetArgs(string spaceshipName, Dictionary<string, int> modifications)
    {
        _spaceshipName = spaceshipName;
        _modifications = modifications;
    }

    public void Execute()
    {
        // Logic to add pieces to the spaceship
        // Example: AddPiece(_spaceshipName, _modifications);
    }

    public bool Verify(IReadOnlyList<string> args)
    {
        // Verification logic
        return true;
    }
}