using System;
using System.Collections.Generic;
using System.IO;
using kdoc.Model;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Framework.Runtime;
using Microsoft.Framework.Runtime.FileSystem;
using Microsoft.Framework.Runtime.Roslyn;

namespace kdoc
{
    public class Program
    {
        private readonly IApplicationEnvironment _appEnvironment;
        private readonly ILibraryExportProvider _exportProvider;
        private readonly IProjectResolver _resolver;

        public Program(IApplicationEnvironment appEnvironment, 
                       ILibraryExportProvider exportProvider,
                       IProjectResolver resolver)
        {
            _appEnvironment = appEnvironment;
            _exportProvider = exportProvider;
            _resolver = resolver;
        }

        public void Main(string[] args)
        {
            // Build the model here
            var model = BuildDocModel();

            Console.ReadLine();
        }

        private DocPackage BuildDocModel()
        {
            var compiler = new RoslynCompiler(_resolver, NoopWatcher.Instance, _exportProvider);
            var compilationContext = compiler.CompileProject(_appEnvironment.ApplicationName, _appEnvironment.TargetFramework);

            var package = new DocPackage("P:" + _appEnvironment.ApplicationName, _appEnvironment.ApplicationName);
            compilationContext.Compilation.Assembly.Accept(
                new DocModelBuilder(package));

            return package;
        }
    }
}
