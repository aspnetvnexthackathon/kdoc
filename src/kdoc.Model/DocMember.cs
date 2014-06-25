namespace kdoc.Model
{
    public abstract class DocMember : DocNode
    {
        public DocMemberKind MemberKind { get; private set; }

        protected DocMember(string docId, string name, DocMemberKind kind) : base(docId, name)
        {
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