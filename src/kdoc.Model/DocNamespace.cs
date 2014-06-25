using System.Collections.Generic;

namespace kdoc
{
    public class DocNamespace : DocNode
    {
        public ICollection<DocType> Types { get; private set; }

        public DocNamespace(string name) : base(name)
        {
            Types = new List<DocType>();
        }
    }
}