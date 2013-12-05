using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ResturantServiceCenter.Models;
using ResturantServiceCenter.DAL;

namespace ResturantServiceCenter.Controllers
{
    public class ItemsServiceController : ApiController
    {
        private ResturantContext db = new ResturantContext();







        // GET api/ItemsService
        public IEnumerable<CategoryItem> GetCategoryItems()
        {
            var items = db.Items.Include(c => c.CategoryMenu);
            return items.AsEnumerable();
        }

        // GET api/ItemsService/5
        public CategoryItem GetCategoryItem(int id)
        {
            CategoryItem categoryitem = db.Items.Find(id);
            if (categoryitem == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return categoryitem;
        }

        // PUT api/ItemsService/5
        public HttpResponseMessage PutCategoryItem(int id, CategoryItem categoryitem)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != categoryitem.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(categoryitem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/ItemsService
        public HttpResponseMessage PostCategoryItem(CategoryItem categoryitem)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(categoryitem);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, categoryitem);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = categoryitem.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/ItemsService/5
        public HttpResponseMessage DeleteCategoryItem(int id)
        {
            CategoryItem categoryitem = db.Items.Find(id);
            if (categoryitem == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Items.Remove(categoryitem);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, categoryitem);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}