using System;

namespace kdoc.models
{
    public class DocProperty : DocMember
    {
	    public string ValueDoc { get; set; }

        public DocProperty(string name) : base(name, DocMemberKind.Property) { }
    }
}