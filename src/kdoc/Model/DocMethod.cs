using System;
using System.Collections.Generic;

namespace kdoc
{
    public class DocMethod : DocMember
    {
        public ICollection<DocParameter> Parameters { get; private set; }

        public DocMethod(string name) : base(name)
        {
            Parameters = new List<DocParameter>();
        }
    }
}