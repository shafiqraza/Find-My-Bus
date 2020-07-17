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
    
    public class BusBookingsController : Controller
    {
        private BusModelContainer db = new BusModelContainer();

        [Authorize]

        // GET: BusBookings
        public ActionResult Index(string search )
        {
            ViewBag.search = search;
            if(search == null)
            {
                if (User.IsInRole("Admin"))
                {
                    var busBookings = db.BusBookings.Include(b => b.Login).Include(b => b.Bus).Include(b => b.BusRoute);
                    return View(busBookings.ToList());
                }else if (User.IsInRole("Employee"))
                {
                    var busBookings = db.BusBookings.Include(b => b.Login).Include(b => b.Bus).Include(b => b.BusRoute);
                    return View(busBookings.ToList());
                }
                else
                {
                    int userId = Convert.ToInt32(Session["currentUserId"]);
                    var busBookings = db.BusBookings.Where(x => x.LoginId == userId).Include(b => b.Login).Include(b => b.Bus).Include(b => b.BusRoute);
                    return View(busBookings.ToList());
                }
                
            }
            else
            {
                return View(db.BusBookings.Where(x => x.Status.Contains(search)).ToList());
            }
        }
        public JsonResult API(string search)
        {
            if (search == null)
            {
                return Json(db.BusBookings.ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(db.BusBookings.Where(x => x.Status.Contains(search)).ToList(),JsonRequestBehavior.AllowGet);
            }
        }

        // GET: BusBookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusBooking busBooking = db.BusBookings.Find(id);
            if (busBooking == null)
            {
                return HttpNotFound();
            }
            return View(busBooking);
        }
        [Authorize(Roles = "Admin,Employee")]
        // GET: BusBookings/Create
        public ActionResult Create()
        {
            ViewBag.LoginId = new SelectList(db.Logins, "Id", "FullName");
            ViewBag.BusId = new SelectList(db.Buses, "Id", "Name");
            ViewBag.BusRoutesId = new SelectList(db.BusRoutes, "Id", "Depature");
            return View();
        }

        // POST: BusBookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "Admin,Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NoOfSeats,Status,DateTime,LoginId,BusId,BusRoutesId")] BusBooking busBooking)
        {
            if (ModelState.IsValid)
            {
                db.BusBookings.Add(busBooking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LoginId = new SelectList(db.Logins, "Id", "FullName", busBooking.LoginId);
            ViewBag.BusId = new SelectList(db.Buses, "Id", "Name", busBooking.BusId);
            ViewBag.BusRoutesId = new SelectList(db.BusRoutes, "Id", "Depature", busBooking.BusRoutesId);
            return View(busBooking);
        }


        // GET: BusBookings/Edit/5
        
        [Authorize(Roles = "Admin,Employee")]
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusBooking busBooking = db.BusBookings.Find(id);
            if (busBooking == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoginId = new SelectList(db.Logins, "Id", "FullName", busBooking.LoginId);
            ViewBag.BusId = new SelectList(db.Buses, "Id", "Name", busBooking.BusId);
            ViewBag.BusRoutesId = new SelectList(db.BusRoutes, "Id", "Depature", busBooking.BusRoutesId);
            return View(busBooking);
        }

        // POST: BusBookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NoOfSeats,Status,DateTime,LoginId,BusId,BusRoutesId")] BusBooking busBooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(busBooking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoginId = new SelectList(db.Logins, "Id", "FullName", busBooking.LoginId);
            ViewBag.BusId = new SelectList(db.Buses, "Id", "Name", busBooking.BusId);
            ViewBag.BusRoutesId = new SelectList(db.BusRoutes, "Id", "Depature", busBooking.BusRoutesId);
            return View(busBooking);
        }

        // GET: BusBookings/Delete/5
        [Authorize(Roles = "Admin,Employee")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusBooking busBooking = db.BusBookings.Find(id);
            if (busBooking == null)
            {
                return HttpNotFound();
            }
            return View(busBooking);
        }
        [Authorize(Roles = "Admin,Employee")]
        // POST: BusBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BusBooking busBooking = db.BusBookings.Find(id);
            db.BusBookings.Remove(busBooking);
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
    }
}
