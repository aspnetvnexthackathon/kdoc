using System;
using System.Collections.Generic;

namespace kdoc.Model
{
    public class DocMethod : DocMember
    {
        public ICollection<DocParameter> Parameters { get; private set; }

        public DocMethod(string docId, string name) : base(docId, name, DocMemberKind.Method)
        {
            Parameters = new List<DocParameter>();
        }
    }
}