using System;

namespace kdoc.Model
{
    public abstract class DocNode
    {
        public string DocId { get; private set; }
        public string Name { get; private set; }
        public string Summary { get; private set; }
        public string Remarks { get; private set; }
        public string Examples { get; private set; }

        protected DocNode(string docId, string name)
        {
            DocId = docId;
            Name = name;
        }
    }
}