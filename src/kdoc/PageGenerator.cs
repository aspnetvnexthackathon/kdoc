using System;
using System.IO;
using System.Threading.Tasks;
using DefaultSiteTemplate;
using kdoc.Model;
using Microsoft.AspNet.TestHost;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Fallback;
using Microsoft.Framework.Runtime;

namespace kdoc
{
    /// <summary>
    /// Summary description for PageGenerator
    /// </summary>
    public class PageGenerator
    {
        private readonly TestClient _client;
        private readonly DocModel _model;

        public PageGenerator(DocModel model,
                             IServiceProvider hostServiceProvider,
                             ILibraryManager libraryManager,
                             IApplicationEnvironment appEnv)
        {
            _model = model;

            var defaultSite = libraryManager.GetLibraryInformation("DefaultSiteTemplate");

            // Replace the IApplicationEnvironment to point to the template site
            var env = ApplicationEnvironment.Create(appEnv);
            env.ApplicationBasePath = Path.GetDirectoryName(defaultSite.Path);
            env.ApplicationName = defaultSite.Name;
            env.TargetFramework = appEnv.TargetFramework;

            var serviceProvider = new ServiceCollection()
                                        .AddInstance<IApplicationEnvironment>(env)
                                        .AddInstance<IDocModelProvider>(new DocModelProvider(model))
                                        .BuildServiceProvider(hostServiceProvider);

            var startup = new DefaultSiteTemplate.Startup();

            // All CreateServices is doing really is setting the ApplicationEnvironment to have paths that MVC expect.
            var testServer = TestServer.Create(serviceProvider, startup.Configure);
            _client = testServer.Handler;
        }

        public async Task GenerateSite(string outputPath)
        {
            foreach (var package in _model.Packages)
            {
                await Write(outputPath, package);
            }

        }

        private async Task Write(string outputPath, DocNode node)
        {

            if (node is DocPackage)
            {
                var package = node as DocPackage;
                await WritePackage(outputPath, package);
            }

            if (node is DocAssembly)
            {
                var assembly = node as DocAssembly;
                await WriteAssembly(outputPath, assembly);
            }

            if (node is DocNamespace)
            {
                var docNamespace = node as DocNamespace;
                await WriteNamespace(outputPath, docNamespace);
            }

            if (node is DocMember)
            {
                var member = node as DocMember;
                await WriteMember(outputPath, member);
            }
        }

        private async Task WriteNamespace(string path, DocNamespace docNamespace)
        {
            await WritePage(Path.Combine(path, docNamespace.Name + ".html"), docNamespace, "Namespace");

            foreach (var type in docNamespace.Types)
            {
                await Write(Path.Combine(path, docNamespace.Name), type);
            }
        }

        private async Task WriteMember(string path, DocMember member)
        {
           
        }

        private async Task WriteAssembly(string path, DocAssembly assembly)
        {
            await WritePage(Path.Combine(path, assembly.Name + ".html"), assembly, "Assembly");

            foreach (var ns in assembly.Namespaces)
            {
                await Write(Path.Combine(path, assembly.Name), ns);
            }
        }

        private async Task WritePackage(string path, DocPackage package)
        {
            await WritePage(Path.Combine(path, package.Name + ".html"), package, "Package");

            foreach (var assembly in package.Assemblies)
            {
                await Write(Path.Combine(path, package.Name), assembly);
            }
        }

        private async Task WritePage(string filePath, DocNode package, string template)
        {
            var html = await CallTemplate(package.DocId, template);
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            File.WriteAllText(filePath, html);

            Console.WriteLine(filePath);
        }

        public async Task<string> CallTemplate(string docId, string template)
        {
            return await _client.GetStringAsync("http://localhost/?docId=" + docId + "&templateName=" + template);
        }
    }
}