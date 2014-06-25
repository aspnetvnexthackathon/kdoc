using System;

namespace kmarkdown
{
    public abstract class MarkdownEngine
    {
        public abstract string Transform(string markdown);
    }
}
