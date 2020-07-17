using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FindMyBus.Models;

namespace FindMyBus.Controllers
{
    public class BusRoutesController : Controller
    {
        private BusModelContainer db = new BusModelContainer();

        // GET: BusRoutes
        public ActionResult Index( string search)
        {
            ViewBag.search = search;
            if(search == null)
            {
                var busRoutes = db.BusRoutes.Include(b => b.Bus);
                return View(busRoutes.ToList());
            }
            else
            {
               var d = db.BusRoutes.Where(x => x.Depature.Contains(search) || x.Arrival.Contains(search)|| x.DepatureDataTime.ToString().Contains(search) || x.ArrivalDateTime.ToString().Contains(search) || x.DepatureDataTime.ToString().Contains(search) || x.ArrivalDateTime.ToString().Contains(search)).ToList();
               
                //db.BusRoutes.Where(x=> x.Arrival)
                return View(d);
            }
            
        }

        public JsonResult API(string search)
        {
            if(search == null)
            {
                return Json(db.BusRoutes.ToList(),JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(db.BusRoutes.Where(x => x.Depature.Contains(search)||x.Arrival.Contains(search)||x.DepatureDataTime.ToString().Contains(search) || x.ArrivalDateTime.ToString().Contains(search)),JsonRequestBehavior.AllowGet);
            }
        }

        // GET: BusRoutes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusRoutes busRoutes = db.BusRoutes.Find(id);
            ViewBag.BusRouteID = busRoutes.Id;
            ViewBag.BusID = busRoutes.BusId;
           
            if (busRoutes == null)
            {
                return HttpNotFound();
            }

           
            return View(busRoutes);
        }

        // GET: BusRoutes/Create
        [Authorize(Roles = "Admin")]

        public ActionResult Create()
        {
            ViewBag.BusId = new SelectList(db.Buses, "Id", "Name");
            return View();
        }

        // POST: BusRoutes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Depature,Arrival,DepatureDataTime,ArrivalDateTime,Price,BusId")] BusRoutes busRoutes)
        {
            if (ModelState.IsValid)
            {
                db.BusRoutes.Add(busRoutes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusId = new SelectList(db.Buses, "Id", "Name", busRoutes.BusId);
            return View(busRoutes);
        }

        // GET: BusRoutes/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusRoutes busRoutes = db.BusRoutes.Find(id);
            if (busRoutes == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusId = new SelectList(db.Buses, "Id", "Name", busRoutes.BusId);
            return View(busRoutes);
        }

        // POST: BusRoutes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Depature,Arrival,DepatureDataTime,ArrivalDateTime,Price,BusId")] BusRoutes busRoutes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(busRoutes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusId = new SelectList(db.Buses, "Id", "Name", busRoutes.BusId);
            return View(busRoutes);
        }

        // GET: BusRoutes/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusRoutes busRoutes = db.BusRoutes.Find(id);
            if (busRoutes == null)
            {
                return HttpNotFound();
            }
            return View(busRoutes);
        }

        // POST: BusRoutes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            BusRoutes busRoutes = db.BusRoutes.Find(id);
            db.BusRoutes.Remove(busRoutes);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Authorize]
        public ActionResult Seats(String Seats, int BusRouteID, int BusID)
        {

            var bookingData = new BusBooking
            {
                Status = "New",
                BusRoutesId = BusRouteID,
                DateTime = DateTime.Now,
                NoOfSeats = Seats,
                LoginId = Convert.ToInt32(Session["currentUserId"]),
                BusId = BusID,
            };

            db.BusBookings.Add(bookingData);
            db.SaveChanges();
            return View(bookingData);
        }
    }
}
