namespace RomanSPA.Demo.Controllers {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using RomanSPA.Core;
    using RomanSPA.Core.Controllers;
    using RomanSPA.Demo.Models;

    public class HomeController : RomanController {

        private RomanSPA.Demo.Models.RomanSPAStarterKitEntities _context;

        public HomeController() : base() {
            // Yes, I'm not using dependency injection for my DB context, cause this is a demo ;-)
            if (_context == null) _context = new Models.RomanSPAStarterKitEntities();
        }

        [RomanPartial]
        public ActionResult Navigation() { return PartialView("~/Views/Partials/Navigation.cshtml"); }

        [RomanPartial]
        public ActionResult Footer() { return PartialView("~/Views/Partials/Footer.cshtml"); }

        [RomanPartial]
        public ActionResult Sidebar() { return PartialView("~/Views/Partials/Sidebar.cshtml"); }

        [RomanAction]
        public ActionResult Index() { return View(new IndexModel()); }

        [RomanAction(Factory=typeof(BlogListFactory), ControllerName="BlogCtrl", ViewPath="/App/views/blog-list.html")]
        public ActionResult Blog() { return View(_context.BlogPosts.ToList()); }

        [RomanAction]
        public ActionResult BlogPost(string slug) {
            var titlesToIds = _context.BlogPosts.ToList().Select(p => new KeyValuePair<int, string>(p.ID, p.Title));

            if (titlesToIds.Any(p => (MakeTitleUrlFriendly(p.Value) == slug))) {
                var post = titlesToIds.First(p => MakeTitleUrlFriendly(p.Value) == slug).Key;
                return View(_context.BlogPosts.First(p => p.ID == post));
            } else {
                return HttpNotFound();
            }
        }

        [RomanAction]
        public ActionResult About() { return View(); }

        private string MakeTitleUrlFriendly(string title) {
            return title.ToLower().Replace(" ", "-");
        }
    }
}