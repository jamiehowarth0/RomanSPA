using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RomanSPA.Core.Models;

namespace RomanSPA.AngularJS.Models {
    public class AngularJsRouteModel : JsRouteModel {
        public string RoutePattern { get; set; }
        public string templateUrl { get; set; }
        public string controller { get; set; }
    }
}
