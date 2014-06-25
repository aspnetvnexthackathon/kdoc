namespace kdoc
{
    public abstract class DocMember
    {
        public string Name { get; private set; }

        protected DocMember(string name)
        {
            Name = name;
        }
    }
}