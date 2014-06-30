using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using RomanSPA.Demo.Models;

namespace RomanSPA.Demo.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using RomanSPA.Demo.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<BlogPost>("BlogPostsApi");
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class BlogPostsApiController : ODataController
    {
        private RomanSPAStarterKitEntities db = new RomanSPAStarterKitEntities();

        // GET: odata/BlogPostsApi
        [Queryable]
        public IQueryable<BlogPost> GetBlogPostsApi()
        {
            return db.BlogPosts;
        }

        // GET: odata/BlogPostsApi(5)
        [Queryable]
        public SingleResult<BlogPost> GetBlogPost([FromODataUri] int key)
        {
            return SingleResult.Create(db.BlogPosts.Where(blogPost => blogPost.ID == key));
        }

        // PUT: odata/BlogPostsApi(5)
        public IHttpActionResult Put([FromODataUri] int key, BlogPost blogPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != blogPost.ID)
            {
                return BadRequest();
            }

            db.Entry(blogPost).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogPostExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(blogPost);
        }

        // POST: odata/BlogPostsApi
        public IHttpActionResult Post(BlogPost blogPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BlogPosts.Add(blogPost);
            db.SaveChanges();

            return Created(blogPost);
        }

        // PATCH: odata/BlogPostsApi(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<BlogPost> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BlogPost blogPost = db.BlogPosts.Find(key);
            if (blogPost == null)
            {
                return NotFound();
            }

            patch.Patch(blogPost);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogPostExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(blogPost);
        }

        // DELETE: odata/BlogPostsApi(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            BlogPost blogPost = db.BlogPosts.Find(key);
            if (blogPost == null)
            {
                return NotFound();
            }

            db.BlogPosts.Remove(blogPost);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BlogPostExists(int key)
        {
            return db.BlogPosts.Count(e => e.ID == key) > 0;
        }
    }
}
