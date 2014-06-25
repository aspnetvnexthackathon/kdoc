namespace kdoc
{
    public abstract class DocMember
    {
        public string Name { get; private set; }
        public DocMemberKind MemberKind { get; private set; }

        protected DocMember(string name, DocMemberKind kind)
        {
            Name = name;
            MemberKind = kind;
        }
    }

    public enum DocMemberKind
    {
        Property,
        Method,
        Field,
        Event,
        Type
    }
}