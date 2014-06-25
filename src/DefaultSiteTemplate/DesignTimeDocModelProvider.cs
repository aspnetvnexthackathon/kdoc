using System;
using kdoc;

namespace DefaultSiteTemplate
{
    internal class DesignTimeDocModelProvider : IDocModelProvider
    {
        public DocNode GetDocModel(string docId)
        {
            if (string.Equals(docId, "SamplePackage"))
            {
                return new DocPackage("Sample Package Name")
                {
                    Summary = "A sample summary for the package"
                };
            }

            return null;
        }
    }
}