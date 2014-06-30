using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RomanSPA.AngularJS;

namespace RomanSPA.AngularJS {
    public static class AngularHtmlHelper {

        public static AngularHtmlString Angular(this HtmlHelper helper) {
            return new AngularHtmlString();
        }
        
        public static AngularHtmlString Angular<T>(this HtmlHelper<T> helper) {
            return new AngularHtmlString();
        }
    }
}