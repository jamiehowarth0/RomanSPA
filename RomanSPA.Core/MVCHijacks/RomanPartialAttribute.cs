namespace RomanSPA.Core {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    public class RomanPartialAttribute : ActionFilterAttribute {

        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            bool executeAction = (filterContext.RequestContext.HttpContext.IsRomanViewRequest() | filterContext.IsChildAction);

            if (executeAction) {
                base.OnActionExecuting(filterContext);
            } else {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
        }
    }
}