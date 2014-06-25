using System;
using System.Collections.Generic;

namespace kdoc.Model
{
    public class DocPackage : DocNode
    {
	    public ICollection<DocAssembly> Assemblies { get; private set; }

        public DocPackage(string docId, string name) : base(docId, name)
        {
            Assemblies = new List<DocAssembly>();
        }
    }
}