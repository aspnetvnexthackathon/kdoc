using System;

namespace kdoc.Model
{
    public class DocField : DocMember
    {
        public string Type { get; private set; }

        public DocField(string docId, string name, string type) : base(docId, name, DocMemberKind.Field)
        {
            Type = type;
        }
	}
}