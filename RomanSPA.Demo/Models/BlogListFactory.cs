using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RomanSPA.Core.Models;

namespace RomanSPA.Demo.Models {
    public class BlogListFactory : IModelFactory {

        private RomanSPAStarterKitEntities _context;

        public BlogListFactory() {
            _context = new RomanSPAStarterKitEntities();
        }

        public object Execute() {
            return _context.BlogPosts.AsQueryable();
        }

    }
}