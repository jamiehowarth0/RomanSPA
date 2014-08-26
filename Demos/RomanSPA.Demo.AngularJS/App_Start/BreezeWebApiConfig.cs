using System.Web.Http;

namespace RomanSPA.Demo.AngularJS {
    ///<summary>
    /// Inserts the Breeze Web API controller route at the front of all Web API routes
    ///</summary>
    public static class BreezeWebApiConfig {

        public static void RegisterBreezePreStart() {
          GlobalConfiguration.Configuration.Routes.MapHttpRoute(
              name: "BreezeApi",
              routeTemplate: "breeze/{controller}/{action}"
          );
        }
    }
}