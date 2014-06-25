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
                var result = await _client.GetStringAsync("http://localhost/?docId=" + package.DocId + "&templateName=Package");
            }
            
        }
    }
}