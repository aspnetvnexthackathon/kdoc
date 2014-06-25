using System;

namespace kdoc.Model
{
    public class DocProperty : DocMember
    {
	    public string ValueDoc { get; set; }

        public string Type { get; set; }

        public DocProperty(string docId, string name, string type) : base(docId, name, DocMemberKind.Property)
        {
            Type = type;
        }
    }
}