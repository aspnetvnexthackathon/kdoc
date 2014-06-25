using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.DependencyInjection;

namespace DefaultSiteTemplate
{
    public class Startup
    {
        public void Configure(IBuilder app)
        {
            var hostDocModelProvider = app.ApplicationServices.GetServiceOrDefault<IDocModelProvider>();

            app.UseServices(services =>
            {
                services.AddMvc();
                                
                if (hostDocModelProvider == null)
                {
                    services.AddSingleton<IDocModelProvider, DesignTimeDocModelProvider>();
                }
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "Default",
                    "",
                    new { controller = "Docs", action = "Index" }
                );
            });
        }
    }
}
