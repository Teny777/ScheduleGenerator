namespace RestrictionAnalyzer.Semant
{
    internal class Type
    {
        private Type(string name)
        {
            Name = name;
        }

        public string Name { get; }

        internal static readonly Type Integer = new Type("Integer");
        internal static readonly Type String = new Type("String");
    }
}