using System;

namespace kdoc
{
    public abstract class DocNode
    {
        public string Name { get; private set; }
        public string Summary { get; private set; }
        public string Remarks { get; private set; }
        public string Examples { get; private set; }

        protected DocNode(string name)
        {
            Name = name;
        }
    }
}