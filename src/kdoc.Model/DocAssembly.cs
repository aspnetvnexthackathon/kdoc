using System.Collections.Generic;

namespace kdoc
{
    public class DocAssembly : DocNode
    {
        public ICollection<DocNamespace> Namespaces { get; private set; }

        public DocAssembly(string name) : base(name)
        {
            Namespaces = new List<DocNamespace>();
        }
    }
}