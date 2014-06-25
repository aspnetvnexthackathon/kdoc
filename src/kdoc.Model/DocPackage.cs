using System;
using System.Collections.Generic;

namespace kdoc
{
    public class DocPackage : DocNode
    {
	    public ICollection<DocAssembly> Assemblies { get; private set; }

        public DocPackage(string name) : base(name)
        {
            Assemblies = new List<DocAssembly>();
        }
    }
}