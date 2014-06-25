using System;
using System.Collections.Generic;
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
            var compiler = new RoslynCompiler(_resolver, NoopWatcher.Instance, _exportProvider);
            var compilationContext = compiler.CompileProject(_appEnvironment.ApplicationName, _appEnvironment.TargetFramework);

            // Build the model here
            // compilationContext.Compilation.GlobalNamespace.Accept();

            Console.ReadLine();
        }
    }
}
