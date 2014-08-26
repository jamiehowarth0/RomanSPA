namespace RomanSPA.AngularJS.Controllers {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using RomanSPA.AngularJS.Models;
    using RomanSPA.Core;
    using RomanSPA.Core.Controllers;
    using RomanSPA.Core.Models;
    using System.Reflection;
    using System.Web.Http;

    public class AngularController : BaseRouteApiController {

        private const string DefaultJsController = "GenericCtrl";

        [HttpGet]
        public override IQueryable<JsRouteModel> Routes() {
            List<AngularJsRouteModel> jsRoutes = new List<AngularJsRouteModel>();
            foreach (var controller in GetControllers()) {
                foreach (var action in GetActionsOnController(controller)) {
                    var metadata = action.GetCustomAttribute<RomanActionAttribute>();
                    jsRoutes.Add(
                        new AngularJsRouteModel() {
                            controller = (!String.IsNullOrEmpty(metadata.ControllerName) ? metadata.ControllerName : DefaultJsController),
                            templateUrl = (!String.IsNullOrEmpty(metadata.ViewPath) ? metadata.ViewPath : GenerateUrl(action.Name, controller.Name)),
                            RoutePattern = GenerateUrl(action.Name, controller.Name)
                        });
                }
            }
            return jsRoutes.AsQueryable();
        }
    }
}
