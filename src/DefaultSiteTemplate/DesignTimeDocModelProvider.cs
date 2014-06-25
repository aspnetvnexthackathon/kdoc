using System;
using System.Linq;
using kdoc.Model;

namespace DefaultSiteTemplate
{
    internal class DesignTimeDocModelProvider : IDocModelProvider
    {
        private readonly Lazy<DocPackage> _samplePackage = new Lazy<DocPackage>(() =>
        {
            var package = new DocPackage("SamplePackage", "Sample Package Name")
            {
                Summary = "A sample summary for the package."
            };

            var assembly = new DocAssembly("SamplePackage.SampleAssembly", "SampleAssembly")
            {
                Summary = "A sample summary for the assembly."
            };

            var sampleNs = new DocNamespace("SamplePackage.SampleAssembly.SampleNamespace", "SampleNamespace")
            {
                Summary = "A sample summary for the namespace."
            };

            var sampleClass = new DocType("SamplePackage.SampleAssembly.SampleClass", "SampleClass", DocTypeKind.Class)
            {
                Summary = "A sample summary for the class."
            };

            var sampleMethod = new DocMethod("SamplePackage.SampleAssembly.SampleClass.SampleMethod", "SampleMethod")
            {
                Summary = "A sample summary for the method."
            };

            var sampleProperty = new DocProperty("SamplePackage.SampleAssembly.SampleClass.SampleProperty", "SampleProperty", "System.String")
            {
                Summary = "A sample summary for the property."
            };

            sampleClass.Members.Add(sampleMethod);
            sampleClass.Members.Add(sampleProperty);
            sampleNs.Types.Add(sampleClass);
            assembly.Namespaces.Add(sampleNs);
            package.Assemblies.Add(assembly);

            return package;
        });

        public DocNode GetDocModel(string docId)
        {
            if (string.Equals(docId, _samplePackage.Value.DocId, StringComparison.OrdinalIgnoreCase))
            {
                return _samplePackage.Value;
            }
            else if (string.Equals(docId, _samplePackage.Value.Assemblies.First().DocId, StringComparison.OrdinalIgnoreCase))
            {
                return _samplePackage.Value.Assemblies.First();
            }
            else if (string.Equals(docId, _samplePackage.Value.Assemblies.First().Namespaces.First().DocId, StringComparison.OrdinalIgnoreCase))
            {
                return _samplePackage.Value.Assemblies.First().Namespaces.First();
            }
            else if (string.Equals(docId, _samplePackage.Value.Assemblies.First().Namespaces.First().Types.First().DocId, StringComparison.OrdinalIgnoreCase))
            {
                return _samplePackage.Value.Assemblies.First().Namespaces.First().Types.First();
            }
            else if (string.Equals(docId, _samplePackage.Value.Assemblies.First().Namespaces.First().Types.First().Members.First().DocId, StringComparison.OrdinalIgnoreCase))
            {
                return _samplePackage.Value.Assemblies.First().Namespaces.First().Types.First().Members.First();
            }

            return null;
        }
    }
}