using System.Collections.Generic;

namespace kdoc
{
    public class DocNamespace
    {
        public string Name { get; private set; }
        public ICollection<DocType> Types { get; private set; }

        public DocNamespace(string name)
        {
            Name = name;
            Types = new List<DocType>();
        }
    }
}