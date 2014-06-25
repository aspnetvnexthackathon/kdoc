namespace kdoc
{
    public class DocParameter : DocNode
    {
        public string Type { get; private set; }

        public DocParameter(string name, string type) : base(name)
        {
            Type = type;
        }
    }
}