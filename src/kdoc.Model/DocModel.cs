using System;
using System.Collections;
using System.Collections.Generic;

namespace kdoc.Model
{
    public class DocModel
    {
        public ICollection<DocPackage> Packages { get; private set; }
        public IDictionary<string, DocNode> Nodes { get; private set; }

        public DocModel()
        {
            Packages = new List<DocPackage>();
            Nodes = new Dictionary<string, DocNode>();
        }

        public void Add(DocNode node)
        {
            Nodes.Add(node.DocId, node);
        }
    }
}