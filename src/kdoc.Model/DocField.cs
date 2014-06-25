using System;

namespace kdoc.models
{
    public class DocField : DocMember
    {
        public string Type { get; private set; }

        public DocField(string name, string type) : base(name, DocMemberKind.Field)
        {
            Type = type;
        }
	}
}