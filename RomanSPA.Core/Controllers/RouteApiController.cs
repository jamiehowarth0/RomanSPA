namespace RomanSPA.Core.Controllers {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;
    using RomanSPA.Core.Controllers;
    using RomanSPA.Core.Models;

    public class RouteApiController : ApiController {

        private const string DefaultJsController = "GenericCtrl";
        
        [System.Web.Http.HttpGet]
        public IQueryable<JsRouteModel> ServerRoutes() {
            
            var allRoutes = RouteTable.Routes
                .Where(route => route.GetType().IsEquivalentTo(typeof(Route)))
                .Cast<Route>().ToList();

            List<JsRouteModel> jsRoutes = new List<JsRouteModel>();
            foreach (var controller in GetControllers()) {
                foreach (var action in GetActionsOnController(controller)) {
                    var metadata = action.GetCustomAttribute<RomanActionAttribute>();
                    jsRoutes.Add(
                        new JsRouteModel() {
                            controller = (!String.IsNullOrEmpty(metadata.ControllerName) ? metadata.ControllerName : DefaultJsController),
                            templateUrl = (!String.IsNullOrEmpty(metadata.ViewPath) ? metadata.ViewPath : GenerateUrl(action.Name, controller.Name)),
                            RoutePattern = GenerateUrl(action.Name, controller.Name)
                        });
                }
            }
            return jsRoutes.AsQueryable();
        }

        private IEnumerable<MethodInfo> GetActionsOnController(Type controller) {
            return controller.GetMethods(BindingFlags.Public | BindingFlags.Instance).Where(method => (method.GetCustomAttribute<RomanActionAttribute>() != null));
        }

        private IEnumerable<Type> GetControllers() {
            var allTypes = new List<Type>();
            var allAsm = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var asm in allAsm) {
                try {
                    allTypes.AddRange(asm.GetExportedTypes().Where(q => q.BaseType != null && q.BaseType.IsEquivalentTo(typeof(RomanController))));
                } catch {
                    continue;
                }
            }
            return allTypes;
        }

        /// <summary>
        ///  This is a WIP for handling more complex routes with parameters. For now we'll just use the <see cref="System.Web.Mvc.UrlHelper"/> to produce a simple serialization of routes.
        /// </summary>
        /// <param name="route">The <see cref="System.Web.Routing.Route"/> to be parsed</param>
        /// <returns>A route prepared for AngularJS routing syntax</returns>
        private string ReplaceTokensWithDefaults(Route route) {
            string url = route.Url;
            foreach (var item in route.Defaults) {
                if (route.Url.IndexOf(String.Format("{{0}}", item.Key)) > -1) {
                    url = url.Replace(String.Format("{{0}}", item.Key), item.Value.ToString());
                }
                // route.Defaults.Where(p => p.Value == UrlParameter.Optional);
            }
            var captureVariables = new Regex(@"\{(?:[^n]+)\}");
            var angularParams = captureVariables.Replace(url, ":{0}");
            return url;
        }

        private string GenerateUrl(string action, string controller) {
            return UrlHelper.GenerateUrl(String.Empty, action, controller.Replace("Controller", String.Empty), null, RouteTable.Routes, GetRequestContext(), true);
        }

        private RequestContext GetRequestContext() {
            HttpContextBase context = new HttpContextWrapper(HttpContext.Current); 
           return new RequestContext(context, RouteTable.Routes.GetRouteData(context)); 
        }
    }
}
