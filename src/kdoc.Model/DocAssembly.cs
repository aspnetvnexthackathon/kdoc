using System.Collections.Generic;

namespace kdoc.Model
{
    public class DocAssembly : DocNode
    {
        public ICollection<DocNamespace> Namespaces { get; private set; }

        public DocAssembly(string docId, string name) : base(docId, name)
        {
            Namespaces = new List<DocNamespace>();
        }
    }
}