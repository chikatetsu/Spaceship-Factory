namespace SpaceshipFactory
{
    internal class Instruction
    {
        public string Header { get; set; }
        public string Value { get; set; }

        public Instruction(string header, string value)
        {
            Header = header;
            Value = value;
        }
    }
}