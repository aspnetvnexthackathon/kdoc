using System;
using System.Collections.Generic;
using System.IO;
using kdoc.Model;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Framework.Runtime;
using Microsoft.Framework.ConfigurationModel;
using System.IO;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.TestHost;
using Microsoft.Framework.DependencyInjection.Fallback;
using Microsoft.Framework.Runtime.FileSystem;
using Microsoft.Framework.Runtime.Roslyn;

namespace kdoc
{
    public class Program
    {
        private readonly IApplicationEnvironment _appEnvironment;
        private IServiceProvider _serviceProvider;
        private readonly ILibraryExportProvider _exportProvider;
        private readonly IProjectResolver _resolver;

        public Program(IApplicationEnvironment appEnvironment, 
                       ILibraryExportProvider exportProvider,
                       IProjectResolver resolver,
					   IServiceProvider serviceProvider)
        {
            _appEnvironment = appEnvironment;
            _serviceProvider = serviceProvider;
            _exportProvider = exportProvider;
            _resolver = resolver;
        }

        public void Main(string[] args)
        {
            // Build the model here
            var model = BuildDocModel();

            Console.ReadLine();
        }

        public async void RunTemplateSite()
        {
            var defaultConfig = new Dictionary<string, string>();

            //Replace the IApplicationEnvironment to point to the template site
            FakeApplicationEnvironment newEnv = FakeApplicationEnvironment.Create(_appEnvironment);
            newEnv.ApplicationBasePath = Path.GetFullPath(@"..\..\src\DefaultSiteTemplate");
            newEnv.ApplicationName = "DefaultSiteTemplate";

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddInstance<IApplicationEnvironment>(newEnv);
            _serviceProvider = serviceCollection.BuildServiceProvider(_serviceProvider);

            //All CreateServices is doing really is setting the ApplicationEnvironment to have paths that MVC expect.
            var testServer = TestServer.Create(_serviceProvider, new DefaultSiteTemplate.Startup().Configure);
            var testClient = testServer.Handler;
            var value = await testClient.GetAsync(@"http://localhost/");
            var result = await value.ReadBodyAsStringAsync();
            Console.WriteLine(result);
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
