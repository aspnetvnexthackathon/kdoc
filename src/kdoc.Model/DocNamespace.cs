using System.Collections.Generic;

namespace kdoc.Model
{
    public class DocNamespace : DocNode
    {
        public ICollection<DocType> Types { get; private set; }

        public DocNamespace(string docId, string name) : base(docId, name)
        {
            Types = new List<DocType>();
        }
    }
}