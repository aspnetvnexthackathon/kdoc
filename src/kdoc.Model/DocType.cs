using System.Collections.Generic;

namespace kdoc
{
    public class DocType : DocMember
    {
        public DocTypeKind TypeKind { get; private set; }
        public ICollection<DocMember> Members { get; private set; }

        public DocType(string name, DocTypeKind typeKind) : base(name, DocMemberKind.Type)
        {
            TypeKind = typeKind;
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