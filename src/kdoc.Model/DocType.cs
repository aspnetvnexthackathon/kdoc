using System.Collections.Generic;

namespace kdoc.Model
{
    public class DocType : DocMember
    {
        public DocTypeKind TypeKind { get; private set; }
        public ICollection<DocMember> Members { get; private set; }

        public DocType(string docId, string name, DocTypeKind typeKind) : base(docId, name, DocMemberKind.Type)
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