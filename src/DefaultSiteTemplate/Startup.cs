using System;
using kdoc.Model;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.DependencyInjection;

namespace DefaultSiteTemplate
{
    public class Startup
    {
        public void Configure(IBuilder app)
        {
            var hostDocModelProvider = app.ApplicationServices.GetServiceOrDefault<IDocModelProvider>();
            var hostDocUrlBuilder = app.ApplicationServices.GetServiceOrDefault<IDocUrlBuilder>();

            app.UseServices(services =>
            {
                services.AddMvc();
                
                if (hostDocModelProvider == null)
                {
                    services.AddSingleton<IDocModelProvider, DesignTimeDocModelProvider>();
                }
                if (hostDocUrlBuilder == null)
                {
                    services.AddSingleton<IDocUrlBuilder, DesignTimeDocUrlBuilder>();
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
