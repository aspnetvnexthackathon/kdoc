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
            app.UseServices(services =>
            {
                services.AddMvc();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "Default",
                    "{controller}/{action}/{index?}",
                    new { controller = "Home", action = "Index" }
                );
            });
        }
    }
}
