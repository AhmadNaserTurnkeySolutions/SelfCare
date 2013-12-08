using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ResturantServiceCenter.Models;
using ResturantServiceCenter.DAL;
using System.Data.Spatial;

namespace ResturantServiceCenter.Controllers
{//http://theoryapp.com/online-store-locator-using-php-mysql-and-google-maps/
    public class LocationController : Controller
    {
        private ResturantContext db = new ResturantContext();

        //
        // GET: /Location/

        public ActionResult Index()
        {
            return View(db.Locations.ToList());
        }

        //
        // GET: /Location/Details/5

        public ActionResult Details(int id = 0)
        {
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        //
        // GET: /Location/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Location/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Location location)
        {
            if (ModelState.IsValid)
            {
                db.Locations.Add(location);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(location);
        }

        //
        // GET: /Location/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        //
        // POST: /Location/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Location location)
        {
            if (ModelState.IsValid)
            {
                db.Entry(location).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(location);
        }

        //
        // GET: /Location/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        //
        // POST: /Location/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Location location = db.Locations.Find(id);
            db.Locations.Remove(location);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }





        public ActionResult SearchingByABoundingBox(double lat0,double lat1,double lng0,double lng1)
        {
        


var locations=from l in db.Locations.ToList() where l.lat>=lat0 && l.lat <=lat1 && l.lng>=lng0 && l.lng <=lng1 select l;


return Json(locations);
        }

        public ActionResult SearchingWithACenter()
        {
            double lat1 = Double.Parse(this.Request.Params.Get("lat"));
            double lng1 = Double.Parse(this.Request.Params.Get("lng"));
              
//SELECT *,( 6371 * acos(cos(radians($lat0)) * cos(radians(lat)) *cos(radians(lng) - radians($lng0)) +
//    sin(radians($lat0)) * sin(radians(lat))
//    ) ) AS distance
//FROM `data_mcdonalds`
//HAVING distance < 10
//ORDER BY distance LIMIT 20;
            var locations = from s in db.Locations.ToList()
                            select new Location
                            {
                                Id = s.Id,
                                address = s.address,
                                lat = s.lat,
                                lng = s.lng,
                              distance = getDistance(s.lat, s.lng, lat1, lng1)
                            };
            locations = from s in locations.ToList<Location>()
                         where s.distance > 1000//km
                         select new Location
                         {
                             Id = s.Id,
                             address = s.address,
                             lat = s.lat,
                             lng = s.lng,
                             distance = s.distance
                         };


            return Json(locations.ToList(), JsonRequestBehavior.AllowGet);
        }


        public double getDistance(double lat0, double lng0, double lat1, double lng1)
    {
        double d = lat0 * 0.017453292519943295;
        double num3 = lng0 * 0.017453292519943295;
        double num4 = lat1 * 0.017453292519943295;
        double num5 = lng1 * 0.017453292519943295;
        double num6 = num5 - num3;
        double num7 = num4 - d;
        double num8 = Math.Pow(Math.Sin(num7 / 2.0), 2.0) + ((Math.Cos(d) * Math.Cos(num4)) * Math.Pow(Math.Sin(num6 / 2.0), 2.0));
        double num9 = 2.0 * Math.Atan2(Math.Sqrt(num8), Math.Sqrt(1.0 - num8));
        return (6376500.0 * num9);
        //   ( 6371 *Math.Acos(Math.Cos(Math radians($lat0)) * cos(radians(lat)) *cos(radians(lng) - radians($lng0)) +sin(radians($lat0)) * sin(radians(lat))))
    }

    }
}