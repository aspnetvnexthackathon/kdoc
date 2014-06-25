namespace kdoc.Model
{
    public class DocParameter : DocNode
    {
        public string Type { get; private set; }

        public DocParameter(string docId, string name, string type) : base(docId, name)
        {
            Type = type;
        }
    }
}