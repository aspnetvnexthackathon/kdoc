using System;

namespace kdoc.Model
{
    public class DocEvent : DocMember
    {
        public string Type { get; private set; }

        public DocEvent(string docId, string name, string type) : base(docId, name, DocMemberKind.Event)
        {
            Type = type;
        }
	}
}