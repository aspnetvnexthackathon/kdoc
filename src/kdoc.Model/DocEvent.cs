using System;

namespace kdoc.models
{
    public class DocEvent : DocMember
    {
        public string Type { get; private set; }

        public DocEvent(string name, string type) : base(name, DocMemberKind.Event)
        {
            Type = type;
        }
	}
}