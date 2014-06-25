using System;
using kdoc.Model;

namespace DefaultSiteTemplate
{
    internal class DesignTimeDocModelProvider : IDocModelProvider
    {
        public DocNode GetDocModel(string docId)
        {
            if (string.Equals(docId, "SamplePackage"))
            {
                return new DocPackage(docId, "Sample Package Name")
                {
                    Summary = "A sample summary for the package"
                };
            }

            return null;
        }
    }
}