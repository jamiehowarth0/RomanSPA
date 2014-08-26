namespace RomanSPA.Core.Controllers {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Web;
    using Http=System.Web.Http;
    using Mvc=System.Web.Mvc;
    using System.Web.Routing;
    using RomanSPA.Core.Controllers;
    using RomanSPA.Core.Models;

    public abstract class BaseRouteApiController : Http.ApiController {

        private const string DefaultJsController = "GenericCtrl";

        [Http.HttpGet]
        public abstract IQueryable<JsRouteModel> Routes();

        [Http.NonAction]
        public virtual IEnumerable<MethodInfo> GetActionsOnController(Type controller) {
            return controller.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                            .Where(method => (method.GetCustomAttribute<RomanActionAttribute>() != null));
        }

        [Http.NonAction]
        public virtual IEnumerable<Type> GetControllers() {
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

        [Http.NonAction]
        public virtual string GenerateUrl(string action, string controller) {
            return Mvc.UrlHelper.GenerateUrl(String.Empty, action, controller.Replace("Controller", String.Empty), null, RouteTable.Routes, GetRequestContext(), true);
        }

        private RequestContext GetRequestContext() {
            HttpContextBase context = new HttpContextWrapper(HttpContext.Current); 
           return new RequestContext(context, RouteTable.Routes.GetRouteData(context)); 
        }
    }
}
