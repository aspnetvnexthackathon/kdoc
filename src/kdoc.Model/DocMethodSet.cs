using System;
using System.Collections.Generic;

namespace kdoc.Model
{
    public class DocMethodSet : DocMember
    {
        public ICollection<DocMethod> Overloads { get; private set; }

        public DocMethodSet(string docId, string name) : base(docId, name, DocMemberKind.MethodSet)
        {
            Overloads = new List<DocMethod>();
        }
    }
}