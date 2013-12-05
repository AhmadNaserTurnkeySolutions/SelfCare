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
    public class ItemController : Controller
    {
        private ResturantContext db = new ResturantContext();

        //
        // GET: /Item/

        public ActionResult Index()
        {
            var items = db.Items.Include(c => c.CategoryMenu);
            return View(items.ToList());
        }

        //
        // GET: /Item/Details/5

        public ActionResult Details(int id = 0)
        {
            CategoryItem categoryitem = db.Items.Find(id);
            if (categoryitem == null)
            {
                return HttpNotFound();
            }
            return View(categoryitem);
        }

        //
        // GET: /Item/Create

        public ActionResult Create()
        {
            ViewBag.CategoryMenuId = new SelectList(db.Categories, "Id", "CategoryName");
            return View();
        }

        //
        // POST: /Item/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryItem categoryitem)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(categoryitem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryMenuId = new SelectList(db.Categories, "Id", "CategoryName", categoryitem.CategoryMenuId);
            return View(categoryitem);
        }

        //
        // GET: /Item/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CategoryItem categoryitem = db.Items.Find(id);
            if (categoryitem == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryMenuId = new SelectList(db.Categories, "Id", "CategoryName", categoryitem.CategoryMenuId);
            return View(categoryitem);
        }

        //
        // POST: /Item/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryItem categoryitem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoryitem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryMenuId = new SelectList(db.Categories, "Id", "CategoryName", categoryitem.CategoryMenuId);
            return View(categoryitem);
        }

        //
        // GET: /Item/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CategoryItem categoryitem = db.Items.Find(id);
            if (categoryitem == null)
            {
                return HttpNotFound();
            }
            return View(categoryitem);
        }

        //
        // POST: /Item/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoryItem categoryitem = db.Items.Find(id);
            db.Items.Remove(categoryitem);
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