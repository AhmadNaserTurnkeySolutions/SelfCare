using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ResturantServiceCenter.Models;
using ResturantServiceCenter.DAL;

namespace ResturantServiceCenter.Controllers
{
    public class CategoryController : Controller
    {
        private ResturantContext db = new ResturantContext();

        //
        // GET: /Category/

        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

                public ActionResult Categories()
        {

return Json(db.Categories.ToList(), JsonRequestBehavior.AllowGet);
        }


 public ActionResult GetAllItemsInCategory(int id = 0)
{

    var itemspercat = db.Items.Where(x => x.CategoryMenuId == id).Select(x => x);

    return Json(itemspercat.ToList(), JsonRequestBehavior.AllowGet);
}


        
        //
        // GET: /Category/Details/5

        public ActionResult Details(int id = 0)
        {
            CategoryMenu categorymenu = db.Categories.Find(id);
            if (categorymenu == null)
            {
                return HttpNotFound();
            }
            return View(categorymenu);
        }

        //
        // GET: /Category/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Category/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryMenu categorymenu)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(categorymenu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categorymenu);
        }

        //
        // GET: /Category/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CategoryMenu categorymenu = db.Categories.Find(id);
            if (categorymenu == null)
            {
                return HttpNotFound();
            }
            return View(categorymenu);
        }

        //
        // POST: /Category/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryMenu categorymenu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categorymenu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categorymenu);
        }

        //
        // GET: /Category/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CategoryMenu categorymenu = db.Categories.Find(id);
            if (categorymenu == null)
            {
                return HttpNotFound();
            }
            return View(categorymenu);
        }

        //
        // POST: /Category/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoryMenu categorymenu = db.Categories.Find(id);
            db.Categories.Remove(categorymenu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }







        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}