namespace SpaceshipFactory
{
    internal class Instruction
    {
        public string Header { get; }
        public string Value { get; }

        public Instruction(string header, string value)
        {
            Header = header;
            Value = value;
        }
    }
}