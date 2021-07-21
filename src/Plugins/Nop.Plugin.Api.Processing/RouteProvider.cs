using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.Api.Processing
{
    public class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            //endpointRouteBuilder.MapControllerRoute(AvalaraTaxDefaults.ConfigurationRouteName, "Plugins/Avalara/Configure",
            //   new { controller = "Avalara", action = "Configure", area = AreaNames.Admin });

            ////override some of default routes in Admin area
            //endpointRouteBuilder.MapControllerRoute("Plugin.Tax.Avalara.Tax.Categories", "Admin/Tax/Categories",
            //    new { controller = "AvalaraTax", action = "Categories", area = AreaNames.Admin });
        }

        public int Priority => -1;
    }
}
