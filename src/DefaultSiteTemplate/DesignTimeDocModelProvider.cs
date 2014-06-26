using System;
using System.Linq;
using kdoc.Model;

namespace DefaultSiteTemplate
{
    internal class DesignTimeDocModelProvider : IDocModelProvider
    {
        private readonly Lazy<DocPackage> _samplePackage = new Lazy<DocPackage>(() =>
        {
            var package = new DocPackage("Pk:SamplePackage", "Sample Package Name")
            {
                Summary = "A sample summary for the package."
            };

            var assembly = new DocAssembly("A:SamplePackage.SampleAssembly", "SampleAssembly")
            {
                Summary = "A sample summary for the assembly."
            };

            var sampleNs = new DocNamespace("N:SamplePackage.SampleAssembly.SampleNamespace", "SampleNamespace")
            {
                Summary = "A sample summary for the namespace."
            };

            var sampleClass = new DocType("T:SamplePackage.SampleAssembly.SampleClass", "SampleClass", DocTypeKind.Class)
            {
                Summary = "A sample summary for the class."
            };

            var sampleMethodSet = new DocMethodSet("Ms:SamplePackage.SampleAssembly.SampleClass.SampleMethod", "SampleMethod")
            {
                Summary = "A sample summary for the method."
            };

            var sampleMethodOverload0 = new DocMethod("M:SamplePackage.SampleAssembly.SampleClass.SampleMethod", "SampleMethod")
            {
                Summary = "A sample summary for the method."
            };

            var sampleMethodOverload1 = new DocMethod("M:SamplePackage.SampleAssembly.SampleClass.SampleMethod(System.String)", "SampleMethod")
            {
                Summary = "A sample summary for the method."
            };

            var sampleProperty = new DocProperty("P:SamplePackage.SampleAssembly.SampleClass.SampleProperty", "SampleProperty", "System.String")
            {
                Summary = "A sample summary for the property."
            };

            var sampleEvent = new DocEvent("E:SamplePackage.SampleAssembly.SampleClass.SampleEvent", "SampleEvent", "System.EventHandler")
            {
                Summary = "A sample summary for the event."
            };

            sampleMethodOverload1.Parameters.Add(new DocParameter("Pa:SamplePackage.SampleAssembly.SampleClass.SampleMethod(System.String)#0", "sampleParameter", "T:System.String"));
            sampleMethodSet.Overloads.Add(sampleMethodOverload0);
            sampleMethodSet.Overloads.Add(sampleMethodOverload1);
            sampleClass.Members.Add(sampleMethodSet);
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
            else if (string.Equals(docId, _samplePackage.Value.Assemblies.First().Namespaces.First().Types.First().Members.First().DocId, StringComparison.OrdinalIgnoreCase))
            {
                return _samplePackage.Value.Assemblies.First().Namespaces.First().Types.First().Members.First();
            }
            else if (string.Equals(docId, ((DocMethodSet)_samplePackage.Value.Assemblies.First().Namespaces.First().Types.First().Members.First()).Overloads.First().DocId, StringComparison.OrdinalIgnoreCase))
            {
                return ((DocMethodSet)_samplePackage.Value.Assemblies.First().Namespaces.First().Types.First().Members.First()).Overloads.First();
            }
            else if (string.Equals(docId, ((DocMethodSet)_samplePackage.Value.Assemblies.First().Namespaces.First().Types.First().Members.First()).Overloads.Skip(1).First().DocId, StringComparison.OrdinalIgnoreCase))
            {
                return ((DocMethodSet)_samplePackage.Value.Assemblies.First().Namespaces.First().Types.First().Members.First()).Overloads.Skip(1).First();
            }
            else if (string.Equals(docId, _samplePackage.Value.Assemblies.First().Namespaces.First().Types.First().Members.Skip(1).First().DocId, StringComparison.OrdinalIgnoreCase))
            {
                return _samplePackage.Value.Assemblies.First().Namespaces.First().Types.First().Members.Skip(1).First();
            }

            return null;
        }
    }
}