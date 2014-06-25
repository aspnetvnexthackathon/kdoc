using System;

namespace kmarkdown
{
    /// <summary>
    /// Summary description for Program
    /// </summary>
    public class Program
    {
        public void Main()
        {
            MarkdownSharp.Markdown m = new MarkdownSharp.Markdown();
            Console.WriteLine(m.Transform("# It works!"));
        }
    }
}