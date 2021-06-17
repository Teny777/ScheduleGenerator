using System.Collections.Generic;

namespace RestrictionAnalyzer.Semant
{
    internal class Scope
    {
        private readonly Dictionary<string, Identifier> _identifiers;

        public Scope()
        {
            _identifiers = new Dictionary<string, Identifier>();
        }

        public Scope PreviousScope { get; set; }

        public void Add(Identifier identifier)
        {
            _identifiers.Add(identifier.Name, identifier);
        }

        public Identifier Search(string name)
        {
            if (name is null) return null;

            if (_identifiers.TryGetValue(name, out Identifier identifier))
            {
                return identifier;
            }
            if (PreviousScope != null)
            {
                return PreviousScope.Search(name);
            }
            return null;
        }
    }
}
