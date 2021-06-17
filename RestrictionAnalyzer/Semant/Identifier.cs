namespace RestrictionAnalyzer.Semant
{
    internal class Identifier
    {
        public string Name { get; }
        public Type Type { get; }

        public Identifier(string name, Type type)
        {
            Name = name;
            Type = type;
        }
    }
}
