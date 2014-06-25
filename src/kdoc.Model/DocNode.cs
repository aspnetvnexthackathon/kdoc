using System;

namespace kdoc.Model
{
    public abstract class DocNode
    {
        public string DocId { get; private set; }
        public string Name { get; private set; }
        public string Summary { get; set; }
        public string Remarks { get; set; }
        public string Examples { get; set; }

        protected DocNode(string docId, string name)
        {
            DocId = docId;
            Name = name;
        }
    }
}