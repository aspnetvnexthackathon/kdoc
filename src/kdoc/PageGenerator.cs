using System;
using System.IO;
using System.Threading.Tasks;
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

        public PageGenerator(IServiceProvider hostServiceProvider,
                             ILibraryManager libraryManager, 
                             IApplicationEnvironment appEnv)
        {
            var defaultSite = libraryManager.GetLibraryInformation("DefaultSiteTemplate");

            // Replace the IApplicationEnvironment to point to the template site
            var env = ApplicationEnvironment.Create(appEnv);
            env.ApplicationBasePath = Path.GetDirectoryName(defaultSite.Path);
            env.ApplicationName = defaultSite.Name;
            env.TargetFramework = appEnv.TargetFramework;

            var serviceProvider = new ServiceCollection()
                                        .AddInstance<IApplicationEnvironment>(env)
                                        .BuildServiceProvider(hostServiceProvider);

            var startup = new DefaultSiteTemplate.Startup();

            // All CreateServices is doing really is setting the ApplicationEnvironment to have paths that MVC expect.
            var testServer = TestServer.Create(serviceProvider, startup.Configure);
            _client = testServer.Handler;
        }

        public async Task GenerateSite(string outputPath)
        {
            var result = await _client.GetStringAsync("http://localhost");
        }
    }
}