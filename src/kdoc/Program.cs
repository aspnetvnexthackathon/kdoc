using System;
using System.Collections.Generic;
using System.IO;
using kdoc.Model;
using Microsoft.Framework.Runtime;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.TestHost;
using Microsoft.Framework.DependencyInjection.Fallback;
using Microsoft.Framework.Runtime.FileSystem;
using Microsoft.Framework.Runtime.Roslyn;
using System.Threading.Tasks;

namespace kdoc
{
    public class Program
    {
        private readonly IApplicationEnvironment _appEnvironment;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILibraryManager _libraryManager;
        private readonly ILibraryExportProvider _exportProvider;
        private readonly IProjectResolver _resolver;

        public Program(IApplicationEnvironment appEnvironment,
                       ILibraryManager libraryManager,
                       ILibraryExportProvider exportProvider,
                       IProjectResolver resolver,
                       IServiceProvider serviceProvider)
        {
            _appEnvironment = appEnvironment;
            _libraryManager = libraryManager;
            _serviceProvider = serviceProvider;
            _exportProvider = exportProvider;
            _resolver = resolver;
        }

        public async Task Main(string[] args)
        {
            // Build the model here
            var model = BuildDocModel();

            string outputPath = Path.Combine(_appEnvironment.ApplicationBasePath, "docs");

            var pageGenerator = new PageGenerator(model, _serviceProvider, _libraryManager, _appEnvironment);
            await pageGenerator.GenerateSite(outputPath);

            Console.ReadLine();
        }

        private DocModel BuildDocModel()
        {
            var compiler = new RoslynCompiler(_resolver, NoopWatcher.Instance, _exportProvider);
            var compilationContext = compiler.CompileProject(_appEnvironment.ApplicationName, _appEnvironment.TargetFramework);

            var model = new DocModel();
            var package = new DocPackage("P:" + _appEnvironment.ApplicationName, _appEnvironment.ApplicationName);
            model.Packages.Add(package);
            compilationContext.Compilation.Assembly.Accept(
                new DocModelBuilder(model, package));

            return model;
        }
    }
}
