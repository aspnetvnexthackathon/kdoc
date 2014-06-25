using System.Collections.Generic;

namespace kdoc
{
    public class DocType : DocNode
    {
        public DocTypeKind Kind { get; private set; }
        public ICollection<DocMember> Members { get; private set; }

        public DocType(string name, DocTypeKind kind) : base(name)
        {
            Kind = kind;
            Members = new List<DocMember>();
        }
    }

    public enum DocTypeKind
    {
        Class,
        Struct,
        Interface,
        Enum,
        Delegate
    }
}