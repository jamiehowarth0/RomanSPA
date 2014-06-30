namespace RomanSPA.Core {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public static class HttpContextBaseExtensions {

        public static bool IsRomanModelRequest(this HttpContextBase context) {
            return context.Request.Headers.AllKeys.Any(p => p.Equals(Keywords.IsRomanModelRequest, StringComparison.InvariantCultureIgnoreCase));
        }

        public static bool IsRomanViewRequest(this HttpContextBase context) {
            return context.Request.Headers.AllKeys.Any(p => p.Equals(Keywords.IsRomanViewRequest, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}