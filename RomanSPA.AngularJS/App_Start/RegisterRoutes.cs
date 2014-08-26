using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using System.Web.Http;
[assembly:WebActivatorEx.PreApplicationStartMethod(typeof(RomanSPA.AngularJS.RegisterRoutes), "Register")]
namespace RomanSPA.AngularJS {
    public class RegisterRoutes {
        public static void Register() {
            RouteTable.Routes.MapHttpRoute("AngularJSRouteTable", "api/Routes/Angular", new { controller = "Angular", action = "Routes" });
        }
    }
}
