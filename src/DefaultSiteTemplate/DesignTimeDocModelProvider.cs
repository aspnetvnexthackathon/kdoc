using System;
using kdoc.Model;

namespace DefaultSiteTemplate
{
    internal class DesignTimeDocModelProvider : IDocModelProvider
    {
        public DocNode GetDocModel(string docId)
        {
            if (string.Equals(docId, "SamplePackage", StringComparison.OrdinalIgnoreCase))
            {
                var package = new DocPackage(docId, "Sample Package Name")
                {
                    Summary = "A sample summary for the package"
                };

                package.Assemblies.Add(new DocAssembly("SamplePackage::SampleAssembly", "SampleAssembly"));

                return package;
            }
            else if (string.Equals(docId, "SamplePackage::SampleAssembly", StringComparison.OrdinalIgnoreCase))
            {
                var docAssembly = new DocAssembly("SamplePackage::SampleAssembly", "SampleAssembly");

                return docAssembly;
            }

            return null;
        }
    }
}